using ZigSimInterpreter.Models.SensorDataComponents;

namespace ZigSimInterpreter.Models
{
    public record SensorData(
        // Motion
        // https://1-10.github.io/zigsim/features/motion.html
        Acceleration? Accel,
        Gravity? Gravity,
        Gyro? Gyro,
        Quaternion? Quaternion,

        // Touch
        // https://1-10.github.io/zigsim/features/touch.html
        Touch[]? Touch,

        // Compass
        // https://1-10.github.io/zigsim/features/compass.html
        CompassData? Compass,

        // GPS
        // https://1-10.github.io/zigsim/features/gps.html
        GPS? GPS,

        // Light
        // No documentation exists for this sensor, but it is present in the ZigSim app
        LightData? Light,

        // ProximityMonitor
        // https://1-10.github.io/zigsim/features/proximity.html
        ProximityMonitorData? ProximityMonitor,

        // Pressure
        // https://1-10.github.io/zigsim/features/pressure.html
        PressureData? Pressure,

        // Mic Level
        // https://1-10.github.io/zigsim/features/mic-level.html
        MicLevel? MicLevel
    );
}
