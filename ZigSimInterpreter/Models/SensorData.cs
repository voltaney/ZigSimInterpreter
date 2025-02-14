using ZigSimInterpreter.Models.SensorDataComponents;

namespace ZigSimInterpreter.Models
{
    public record SensorData(
            // Motion
            // https://1-10.github.io/zigsim/features/motion.html
            Acceleration? Accel = null,
            Gravity? Gravity = null,
            Gyro? Gyro = null,
            Quaternion? Quaternion = null,

            // Touch
            // https://1-10.github.io/zigsim/features/touch.html
            IEnumerable<Touch>? Touch = null,

            // Compass
            // https://1-10.github.io/zigsim/features/compass.html
            CompassData? Compass = null,

            // GPS
            // https://1-10.github.io/zigsim/features/gps.html
            GPS? GPS = null,

            // Light
            // No documentation exists for this sensor, but it is present in the ZigSim app
            LightData? Light = null,

            // ProximityMonitor
            // https://1-10.github.io/zigsim/features/proximity.html
            ProximityMonitorData? ProximityMonitor = null,

            // Pressure
            // https://1-10.github.io/zigsim/features/pressure.html
            PressureData? Pressure = null,

            // Mic Level
            // https://1-10.github.io/zigsim/features/mic-level.html
            MicLevel? MicLevel = null
        )
    {
        public virtual bool Equals(SensorData? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Accel == other.Accel &&
                   Gravity == other.Gravity &&
                   Gyro == other.Gyro &&
                   Quaternion == other.Quaternion &&
                   (Touch == other.Touch || (Touch != null && other.Touch != null && Touch.SequenceEqual(other.Touch))) &&
                   Compass == other.Compass &&
                   GPS == other.GPS &&
                   Light == other.Light &&
                   ProximityMonitor == other.ProximityMonitor &&
                   Pressure == other.Pressure &&
                   MicLevel == other.MicLevel;
        }

        public override int GetHashCode()
        {
            return (
                Accel, Gravity, Gyro, Quaternion,
                Touch, Compass, GPS, Light,
                ProximityMonitor, Pressure, MicLevel
            ).GetHashCode();
        }
    }
}
