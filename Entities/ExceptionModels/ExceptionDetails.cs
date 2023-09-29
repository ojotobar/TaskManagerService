using System.Text.Json;

namespace Entities.ExceptionModels
{
    public class ExceptionDetails
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
