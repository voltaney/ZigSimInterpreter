namespace ZigSimInterpreter.Models.SensorDataComponents
{
    public record CompassData(
        double Compass,
        int Faceup
    )
    {
        public bool IsFaceup => Faceup == 1;
    }
}
