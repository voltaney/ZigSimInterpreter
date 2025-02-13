namespace ZigSimInterpreter.Models
{
    public record DeviceInfo(
        string? OS,
        string? OsVersion,
        string? Name,
        string? UUID,
        int? DisplayWidth,
        int? DisplayHeight
    );
}
