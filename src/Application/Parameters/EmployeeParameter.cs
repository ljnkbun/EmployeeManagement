using Core.Models.Requests;

namespace Application.Parameters
{
    public class EmployeeParameter : RequestParameter
    {
        public string? Name { get; set; }
        public string? Username { get; set; }
    }
}
