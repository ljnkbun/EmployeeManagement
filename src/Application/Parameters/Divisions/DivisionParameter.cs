using Core.Models.Requests;

namespace Application.Parameters.Divisions
{
    public class DivisionParameter : RequestParameter
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
    }
}
