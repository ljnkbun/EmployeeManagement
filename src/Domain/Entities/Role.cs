using Core.Models.Entities;

namespace Domain.Entities
{
    public class Role : BaseEntity
    {
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public ICollection<EmployeeRole>? EmployeeRoles { get; set; }
    }
}
