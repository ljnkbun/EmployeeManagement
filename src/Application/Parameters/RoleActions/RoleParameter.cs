using Core.Models.Requests;

namespace Application.Parameters.RoleActions
{
    public class RoleActionParameter : RequestParameter
    {
        public string Controller { get; set; } = default!;
        public string Action { get; set; } = default!;
        public int RoleId { get; set; }
    }
}
