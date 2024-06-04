using Core.Models.Models;

namespace Application.Models.Roles
{
    public class RoleModel : BaseModel
    {
        public string Name { get; set; } = default!;
        public string Code { get; set; } = default!;
    }
}
