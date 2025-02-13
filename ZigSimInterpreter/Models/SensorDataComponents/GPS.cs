using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZigSimInterpreter.Models.SensorDataComponents
{
    public record GPS(
        double Latitude,
        double Longitude
    );
}
