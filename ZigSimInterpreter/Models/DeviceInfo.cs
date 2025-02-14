namespace ZigSimInterpreter.Models
{
    public record DeviceInfo(
        string? OS,
        string? OSVersion,
        string? Name,
        string? UUID,
        int? DisplayWidth,
        int? DisplayHeight
    );
}
