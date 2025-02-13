namespace ZigSimInterpreter.Models.SensorDataComponents
{
    public record Acceleration(
        double X,
        double Y,
        double Z
    );

    public record Gravity(
        double X,
        double Y,
        double Z
    );

    public record Gyro(
        double X,
        double Y,
        double Z
    );

    public record Quaternion(
        double X,
        double Y,
        double Z,
        double W
    );

}
