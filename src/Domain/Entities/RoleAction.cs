using Core.Models.Entities;

namespace Domain.Entities
{
    public class RoleAction : BaseEntity
    {
        public string Controller { get; set; } = default!;
        public string Action { get; set; } = default!;
        public int RoleId { get; set; } = default!;
        public virtual Role Role { get; set; } = default!;
    }
}
