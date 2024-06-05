using Core.Models.Models;

namespace Application.Models.ControllerActions
{
    public class ControllerActionModel : BaseModel
    {
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Controller { get; set; } = default!;
        public string Action { get; set; } = default!;
    }
}
