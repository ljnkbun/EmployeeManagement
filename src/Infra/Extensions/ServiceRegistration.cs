using Domain.Interface;
using Infra.Contexts;
using Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infra.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
            services.AddDbContext<EmployeeManagementDBContext>(options => options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")!)
                      .EnableSensitiveDataLogging());

            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IAppUserRepository, AppUserRepository>();
            services.AddTransient<IDivisionRepository, DivisionRepository>();
            services.AddTransient<IUserRoleRepository, UserRoleRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IRoleActionRepository, RoleActionRepository>();
            services.AddTransient<IControllerActionRepository, ControllerActionRepository>();
        }
    }
}
