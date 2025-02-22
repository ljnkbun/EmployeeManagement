﻿using Core.Models.Entities;

namespace Domain.Entities
{
    public class Role : BaseEntity
    {
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public virtual ICollection<UserRole>? UserRoles { get; set; }
        public virtual ICollection<RoleAction>? RoleActions { get; set; }
    }
}
