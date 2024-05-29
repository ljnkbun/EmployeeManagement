namespace Domain.Entities
{
    public class Employee : AppUser
    {
        public string Code { get; set; } = default!;
        public int? DivisionId { get; set; }
        public virtual Division? Division { get; set; }
    }
}
