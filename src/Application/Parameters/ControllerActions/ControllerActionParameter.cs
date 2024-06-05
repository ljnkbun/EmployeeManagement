using Core.Models.Requests;

namespace Application.Parameters.ControllerActions
{
    public class ControllerActionParameter : RequestParameter
    {
        public string? Code { get; set; } 
        public string? Name { get; set; }
        public string? Controller { get; set; } 
        public string? Action { get; set; } 
    }
}
