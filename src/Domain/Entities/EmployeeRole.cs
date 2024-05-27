using Core.Models.Entities;

namespace Domain.Entities
{
    public class EmployeeRole : BaseEntity
    {
        public int EmployeeId { get; set; } = default!;
        public Employee Employee { get; set; } = default!;
        public int RoleId { get; set; } = default!;
        public Role Role { get; set; } = default!;
    }
}
