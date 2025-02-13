using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
