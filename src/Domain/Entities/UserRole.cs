using Core.Models.Entities;

namespace Domain.Entities
{
    public class UserRole : BaseEntity
    {
        public int AppUserId { get; set; } = default!;
        public virtual AppUser AppUser { get; set; } = default!;
        public int RoleId { get; set; } = default!;
        public virtual Role Role { get; set; } = default!;
    }
}
