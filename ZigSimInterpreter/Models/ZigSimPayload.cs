namespace ZigSimInterpreter.Models
{
    public record ZigSimPayload(
        DeviceInfo? Device = null,
        // Treat TimeStamp as a raw string because the format depends on devices
        string? TimeStamp = null,
        SensorData? SensorData = null
    );
}
