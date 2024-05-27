using System.Linq.Expressions;
using System.Reflection;

namespace Core.Extensions.Objects
{
    public static class QueryableExtensions
    {
        public static IQueryable<TEntity> Paged<TEntity>(this IQueryable<TEntity> query, int pageSize, int pageNumber)
        {
            return pageSize <= 0 ? query : query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
        public static IQueryable<TEntity> Filter<TEntity, TParams>(this IQueryable<TEntity> query, TParams requestParameter)
        {
            Type paramsType = typeof(TParams);
            Type entityType = typeof(TEntity);

            PropertyInfo[] paramPropArr = paramsType.GetProperties();
            PropertyInfo[] entityPropArr = entityType.GetProperties();

            Expression mainExpression = null!;
            var entityParameter = Expression.Parameter(entityType, entityType.Name.ToLower());
            foreach (PropertyInfo paramProp in paramPropArr)
            {
                object paramValue = paramProp.GetValue(requestParameter)!;
                PropertyInfo entityProp = entityPropArr.FirstOrDefault(n => n.Name.Equals(paramProp.Name));
                if (entityProp == null || IsNullable(paramProp, paramValue))
                {
                    continue;
                }

                ConstantExpression constantExpression = Expression.Constant(paramValue, entityProp.PropertyType);
                Expression entityMemberExpression = Expression.Property(entityParameter, entityProp);
                Expression subExpression = null!;

                if (paramProp.PropertyType != typeof(string))
                {
                    subExpression = Expression.Equal(entityMemberExpression, constantExpression);
                }
                else
                {
                    subExpression = Expression.Call(entityMemberExpression, containsMethod, constantExpression);
                }

                if (subExpression != null)
                {
                    mainExpression = mainExpression == null ? subExpression : Expression.And(mainExpression, subExpression);
                }
            }
            return mainExpression == null ? query : query.Where(Expression.Lambda<Func<TEntity, bool>>(mainExpression, entityParameter));
        }
        public static IQueryable<TEntity> Filters<TEntity, TParams>(this IQueryable<TEntity> query, List<TParams> requestParameters)
        {
            Type paramsType = typeof(TParams);
            Type entityType = typeof(TEntity);

            PropertyInfo[] paramPropArr = paramsType.GetProperties();
            PropertyInfo[] entityPropArr = entityType.GetProperties();

            Expression mainExpression = null!;
            var entityParameter = Expression.Parameter(entityType, entityType.Name.ToLower());
            foreach (var requestParameter in requestParameters)
            {
                Expression groupExpresion = null!;
                foreach (PropertyInfo paramProp in paramPropArr)
                {
                    object paramValue = paramProp.GetValue(requestParameter)!;
                    PropertyInfo entityProp = entityPropArr.FirstOrDefault(n => n.Name.Equals(paramProp.Name));
                    if (entityProp == null || IsNullable(paramProp, paramValue))
                    {
                        continue;
                    }

                    ConstantExpression constantExpression = Expression.Constant(paramValue, entityProp.PropertyType);
                    Expression entityMemberExpression = Expression.Property(entityParameter, entityProp);
                    Expression subExpression = null!;

                    if (paramProp.PropertyType != typeof(string))
                    {
                        subExpression = Expression.Equal(entityMemberExpression, constantExpression);
                    }
                    else
                    {
                        subExpression = Expression.Call(entityMemberExpression, containsMethod, constantExpression);
                    }

                    if (subExpression != null)
                    {
                        groupExpresion = groupExpresion == null ? subExpression : Expression.And(groupExpresion, subExpression);
                    }
                }
                mainExpression = mainExpression == null ? groupExpresion : Expression.Or(mainExpression, groupExpresion);
            }

            return mainExpression == null ? query : query.Where(Expression.Lambda<Func<TEntity, bool>>(mainExpression, entityParameter));
        }
        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> query, string orderBy)
        {
            foreach (OrderByInfo orderByInfo in ParseOrderBy(orderBy))
                query = ApplyOrderBy(query, orderByInfo);
            return query;
        }

        public static IQueryable<TEntity> SearchTerm<TEntity>(this IQueryable<TEntity> query, string searchTerm, List<string> searchProps)
        {
            if (string.IsNullOrEmpty(searchTerm)) return query;

            Type entityType = typeof(TEntity);
            PropertyInfo[] entityPropArr = entityType.GetProperties();

            if (searchProps != null && searchProps.Count > 0)
            {
                entityPropArr = entityPropArr.Where(x => searchProps.Contains(x.Name)).ToArray();
            }

            Expression mainExpression = null!;
            var entityParameter = Expression.Parameter(entityType, entityType.Name.ToLower());
            var isEqualNonUnicode = searchTerm == searchTerm.ToNonUnicode();
            foreach (PropertyInfo entityProp in entityPropArr)
            {
                if (entityProp.PropertyType == typeof(string))
                {
                    ConstantExpression constantExpression = Expression.Constant(searchTerm, entityProp.PropertyType);
                    Expression entityMemberExpression = Expression.Property(entityParameter, entityProp);
                    Expression subExpression = null!;

                    subExpression = Expression.Call(entityMemberExpression, containsMethod, constantExpression);

                    if (subExpression != null)
                    {
                        mainExpression = mainExpression == null ? subExpression : Expression.Or(mainExpression, subExpression);
                    }
                    if (!isEqualNonUnicode)
                    {
                        constantExpression = Expression.Constant(searchTerm.ToNonUnicode(), entityProp.PropertyType);
                        subExpression = Expression.Call(entityMemberExpression, containsMethod, constantExpression);

                        if (subExpression != null)
                        {
                            mainExpression = mainExpression == null ? subExpression : Expression.Or(mainExpression, subExpression);
                        }
                    }
                }
            }
            return mainExpression == null ? query : query.Where(Expression.Lambda<Func<TEntity, bool>>(mainExpression, entityParameter));
        }

        private static IQueryable<TEntity> ApplyOrderBy<TEntity>(IQueryable<TEntity> query, OrderByInfo orderByInfo)
        {
            string[] props = orderByInfo.PropertyName!.Split('.');
            Type type = typeof(TEntity);

            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                PropertyInfo pi = type.GetProperty(prop, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                if (pi != null)
                {
                    expr = Expression.Property(expr, pi);
                    type = pi.PropertyType;
                }
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(TEntity), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);
            string methodName = string.Empty;

            if (!orderByInfo.Initial && query is IOrderedQueryable<TEntity>)
            {
                if (orderByInfo.Direction == SortDirection.Ascending)
                    methodName = "ThenBy";
                else
                    methodName = "ThenByDescending";
            }
            else
            {
                if (orderByInfo.Direction == SortDirection.Ascending)
                    methodName = "OrderBy";
                else
                    methodName = "OrderByDescending";
            }

            return (IOrderedQueryable<TEntity>)typeof(Queryable).GetMethods().Single(
                method => method.Name == methodName
                        && method.IsGenericMethodDefinition
                        && method.GetGenericArguments().Length == 2
                        && method.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(TEntity), type)
                .Invoke(null, [query, lambda])!;

        }
        private static IEnumerable<OrderByInfo> ParseOrderBy(string orderBy)
        {
            if (string.IsNullOrEmpty(orderBy))
                yield break;

            string[] items = orderBy.Split(',');
            bool initial = true;
            foreach (string item in items)
            {
                string[] pair = item.Trim().Split(' ');

                if (pair.Length > 2)
                    throw new ArgumentException(string.Format("Invalid OrderBy string '{0}'. Order By Format: Property, Property2 ASC, Property2 DESC", item));

                string prop = pair[0].Trim();

                if (string.IsNullOrEmpty(prop))
                    throw new ArgumentException("Invalid Property. Order By Format: Property, Property2 ASC, Property2 DESC");

                SortDirection dir = SortDirection.Ascending;

                if (pair.Length == 2)
                    dir = "desc".Equals(pair[1].Trim(), StringComparison.OrdinalIgnoreCase) ? SortDirection.Descending : SortDirection.Ascending;

                yield return new OrderByInfo() { PropertyName = prop, Direction = dir, Initial = initial };

                initial = false;
            }

        }
        private sealed class OrderByInfo
        {
            public string? PropertyName { get; set; }
            public SortDirection Direction { get; set; }
            public bool Initial { get; set; }
        }
        private enum SortDirection
        {
            Ascending = 0,
            Descending = 1
        }
        private static bool IsNullable(PropertyInfo prop, object value)
        {
            if (prop.PropertyType != typeof(string))
            {
                return value == null;
            }
            return value == null || string.IsNullOrEmpty(value.ToString());
        }
        private static readonly MethodInfo containsMethod = typeof(string).GetMethod("Contains", [typeof(string)])!;
    }
}
