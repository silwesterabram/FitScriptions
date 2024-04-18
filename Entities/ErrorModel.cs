using System.Text.Json;

namespace Entities
{
    public class ErrorDetails
    {
        public string? message { get; init; } = null!;
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
