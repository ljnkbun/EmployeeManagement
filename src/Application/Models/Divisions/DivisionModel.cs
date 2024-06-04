using Core.Models.Models;

namespace Application.Models.Divisions
{
    public class DivisionModel : BaseModel
    {
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
    }
}
