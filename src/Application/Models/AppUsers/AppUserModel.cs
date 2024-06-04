using Core.Models.Models;

namespace Application.Models.AppUsers
{
    public class AppUserModel : BaseModel
    {
        public string Token { get; set; } = default!;
    }
}
