using Core.Models.Entities;

namespace Domain.Entities
{
    public class Test : BaseEntity
    {
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
    }
}
