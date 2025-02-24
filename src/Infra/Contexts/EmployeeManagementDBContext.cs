using Core.Models.Entities;
using Domain.Entities;
using Infra.TypeConfigurations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infra.Contexts
{
    public class EmployeeManagementDBContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmployeeManagementDBContext(DbContextOptions<EmployeeManagementDBContext> options
            , IHttpContextAccessor httpContextAccessor
            ) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _httpContextAccessor = httpContextAccessor;
        }

        public EmployeeManagementDBContext() { }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var curUserId = _httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(x => x.Type == "id")!.Value;
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = int.Parse(curUserId);

                        entry.Entity.UpdatedDate = DateTime.Now;
                        entry.Entity.UpdatedBy = int.Parse(curUserId);
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedDate = DateTime.Now;
                        entry.Entity.UpdatedBy = int.Parse(curUserId);
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Division> Divisions { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<ControllerAction> ControllerActions { get; set; }
        //public virtual DbSet<Test> Tests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new DivisionConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new ControllerActionConfiguration());

            //foreach (var entity in modelBuilder.Model.GetEntityTypes())
            //{
            //    if (entity.ClrType.Name == typeof(Test).Name)
            //    {
            //        entity.SetSchema("ABCD");
            //        entity.SetTableName(typeof(Test).Name);
            //    }
            //}
            ////modelBuilder.Entity<Test>().ToTable(typeof(Test).Name, "ABCD").HasKey(m => m.Id);
            //modelBuilder.ApplyConfiguration(new TestConfiguration());
        }
    }
}
