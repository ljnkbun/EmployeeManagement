using Core.Models.Models;

namespace Application.Models.RoleActions
{
    public class RoleActionModel : BaseModel
    {
        public string Controller { get; set; } = default!;
        public string Action { get; set; } = default!;
        public int RoleId { get; set; }
    }
}
