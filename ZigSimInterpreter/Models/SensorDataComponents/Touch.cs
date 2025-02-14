namespace ZigSimInterpreter.Models.SensorDataComponents
{
    public record Touch(
        double X,
        double Y,
        // The following fields are optional
        double? Radius,
        double? Force
    );
}
