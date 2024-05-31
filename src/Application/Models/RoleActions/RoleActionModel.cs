namespace Application.Models.RoleActions
{
    public class RoleActionModel
    {
        public int Id { get; set; }
        public string Controller { get; set; } = default!;
        public string Action { get; set; } = default!;
        public int RoleId { get; set; }
    }
}
