using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZigSimInterpreter.Models.SensorDataComponents
{
    public record MicLevel(
        double Max,
        double Average
    );
}
