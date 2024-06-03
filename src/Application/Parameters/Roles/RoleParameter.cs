using Core.Models.Requests;

namespace Application.Parameters.Roles
{
    public class RoleParameter : RequestParameter
    {
        public int? Id { get; set; } 
        public string? Name { get; set; } 
        public string? Code { get; set; }
    }
}
