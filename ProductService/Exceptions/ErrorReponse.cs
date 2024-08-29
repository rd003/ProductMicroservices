
using System.Text.Json;

namespace ProductService.Exceptions;

public class ErrorReponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
