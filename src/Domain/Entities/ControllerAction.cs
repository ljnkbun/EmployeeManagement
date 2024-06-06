using Core.Models.Entities;

namespace Domain.Entities
{
    public class ControllerAction : BaseEntity
    {
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!; 
        public string Controller { get; set; } = default!;
        public string Action { get; set; } = default!;
        public virtual ICollection<RoleAction>? RoleActions { get; set; }
    }
}
