using Core.Models.Entities;

namespace Domain.Entities
{
    public class Division : BaseEntity
    {
        public string Name { get; set; } = default!;
        public ICollection<Employee>? Employees { get; set; }
    }
}
