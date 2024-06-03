namespace Application.Models.Employees
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public int DivisionId { get; set; }
        public int[]? RoleIds { get; set; }
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
