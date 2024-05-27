﻿namespace Core.Models.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool IsDel { get; set; } 
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
