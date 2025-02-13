using ZigSimInterpreter.Models;

namespace ZigSimInterpreter
{
    public record ZigSimResult(
        ZigSimPayload? Payload = null,
        bool IsSuccess = false,
        string? ErrorMessage = null
    );
}
