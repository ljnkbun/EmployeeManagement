using System.Collections;
using System.Text.Json;

namespace Core.Extensions.Objects
{
    public static class ObjectExtension
    {
        public static T Clone<T>(this T obj)
        {
            var serialized = JsonSerializer.Serialize(obj);
            return JsonSerializer.Deserialize<T>(serialized)!;
        }
        public static bool IsNullOrEmpty(this ICollection coll)
        {
            if (coll == null || coll.Count == 0) return true;
            return false;
        }
        public static bool IsNumber(this object value)
        {
            return value is sbyte
                    || value is byte
                    || value is short
                    || value is ushort
                    || value is int
                    || value is uint
                    || value is long
                    || value is ulong
                    || value is float
                    || value is double
                    || value is decimal;
        }

        public static object GetPropValue<T>(this T type, string field) where T : class
        {
            var property =
                type.GetType().GetProperties().FirstOrDefault(
                    pi => string.Equals(field, pi.Name, StringComparison.OrdinalIgnoreCase));
            return property?.GetValue(type) ?? string.Empty;
        }
        public static void TransferProperties<T>(this T obj, object target, IList<string> ignoreProperties = null!)
        {
            var prods = obj!.GetType().GetProperties();
            foreach (var prop in prods)
            {
                if (ignoreProperties?.Contains(prop.Name) == true)
                    continue;
                var ent = target.GetType().GetProperty(prop.Name);
                if (ent == null || !ent.CanRead) continue;
                var val = prop.GetValue(obj, null);
                ent.SetValue(target, val, null);
            }
        }
        public static void SetValue(this object obj, string fieldName, object value)
        {
            var prod = obj.GetType().GetProperty(fieldName);
            if (prod == null || !prod.CanRead) return;
            var t = prod.PropertyType;
            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null || value.Equals(""))
                {
                    prod.SetValue(obj, null, null);
                    return;
                }
                t = Nullable.GetUnderlyingType(t);
            }
            var val = Convert.ChangeType(value, t!);
            prod.SetValue(obj, val, null);
        }
    }
}
