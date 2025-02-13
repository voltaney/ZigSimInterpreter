using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
