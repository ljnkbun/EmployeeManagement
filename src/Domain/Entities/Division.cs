using Core.Models.Entities;

namespace Domain.Entities
{
    public class Division : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Code { get; set; } = default!;
        public virtual ICollection<Employee>? Employees { get; set; }
    }
}
