using Core.Models.Entities;

namespace Domain.Entities
{
    public class AppUser : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public virtual ICollection<UserRole>? UserRoles { get; set; }
    }
}
