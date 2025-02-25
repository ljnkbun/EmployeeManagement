using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infra.Contexts
{
    public class EmployeeManagementDBContextFactory : IDesignTimeDbContextFactory<EmployeeManagementDBContext>
    {
        public EmployeeManagementDBContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<EmployeeManagementDBContext>();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

            return new EmployeeManagementDBContext(optionsBuilder.Options, null!);
        }
    }
}
