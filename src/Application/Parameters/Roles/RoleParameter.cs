using Core.Models.Requests;

namespace Application.Parameters.Roles
{
    public class RoleParameter : RequestParameter
    {
        public string? Name { get; set; } 
        public string? Code { get; set; }
    }
}
