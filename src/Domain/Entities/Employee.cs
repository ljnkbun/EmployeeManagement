using Core.Models.Entities;

namespace Domain.Entities
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public int? DivisionId { get; set; }
        public Division? Division { get; set; }
        public ICollection<EmployeeRole>? EmployeeRoles { get; set; }
    }
}
