using Core.Models.Requests;

namespace Application.Parameters.Employees
{
    public class EmployeeParameter : RequestParameter
    {
        public string? Name { get; set; }
        public string? Username { get; set; }
        public int? DivisionId { get; set; }
    }
}
