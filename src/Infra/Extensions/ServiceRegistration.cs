using Domain.Interface;
using Infra.Contexts;
using Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EmployeeManagementDBContext>(options => options.UseMySQL(
                configuration.GetConnectionString("DefaultConnection")!,
                b => b.MigrationsAssembly(typeof(EmployeeManagementDBContext).Assembly.FullName)
                      .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
                      .EnableSensitiveDataLogging());

            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IDivisionRepository, DivisionRepository>();
            services.AddTransient<IEmployeeRoleRepository, EmployeeRoleRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
        }
    }
}
