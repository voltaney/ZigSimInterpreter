# ZigSimInterpreter

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![NuGet Version](https://img.shields.io/nuget/v/Voltaney.ZigSimInterpreter)](https://www.nuget.org/packages/Voltaney.ZigSimInterpreter/)
[![CI](https://github.com/voltaney/ZigSimInterpreter/actions/workflows/ci.yml/badge.svg)](https://github.com/voltaney/ZigSimInterpreter/actions/workflows/ci.yml)
![dotnet Version](https://img.shields.io/badge/.NET-8.0-blueviolet)

Parser library for data sent from the [ZigSim mobile application](https://zig-project.com/). Currently supports **only JSON** format.

## Usage

Example of receiving data from a ZigSim app via UDP and parsing its contents to display Gyro sensor values.

```csharp
using System.Net;
using System.Net.Sockets;
using ZigSimInterpreter;

namespace ConsoleApp1;

class Program
{
    static void Main(string[] args)
    {
        int port = 50000;
        IPEndPoint? senderEP = null;
        var interpreter = new ZigSimJsonInterpreter();

        // Ctrl + C to quit
        using (var udpClient = new UdpClient(port))
        {
            while (true)
            {
                var recievedData = udpClient.Receive(ref senderEP);
                // parse recieved data of ZigSim
                var zigSimResult = interpreter.Read(recievedData);
                if (zigSimResult.IsSuccess)
                {
                    // print Gyro data
                    Console.WriteLine(zigSimResult.Payload?.SensorData?.Gyro);
                }
                else
                {
                    // possibly recieved other format (ex. OSC)
                    Console.WriteLine("Failed to parse ZigSim data");
                    Console.WriteLine(zigSimResult.ErrorMessage);
                }
            }
        }
    }
}

```

## Payload Structure

If parsing is successful, data is stored in the Payload member.

```csharp
var interpreter = new ZigSimJsonInterpreter();
var zigSimResult = interpreter.Read(recievedData);
var payload = zigSimResult.Payload;
```

The structure of Payload is as follows.
If the parameter does not exist, its attribute is NULL.

```json
{
  "Device": {
    "OS": "ios",
    "OSVersion": "XXX",
    "Name": "iPhone XXX",
    "UUID": "XXX",
    "DisplayWidth": 750,
    "DisplayHeight": 1334
  },
  "TimeStamp": "2025_02_21_15:46:34.574",
  "SensorData": {
    "Accel": {
      "X": -0.013299062848091125,
      "Y": 0.0030906200408935547,
      "Z": -0.008916616439819336
    },
    "Gravity": {
      "X": -0.024832651019096375,
      "Y": -0.36426615715026855,
      "Z": -0.9309637546539307
    },
    "Gyro": {
      "X": 0.006727981381118298,
      "Y": -0.02589605376124382,
      "Z": 0.008115466684103012
    },
    "Quaternion": {
      "X": 0.18086875635035513,
      "Y": 0.042480770317941945,
      "Z": 0.28920687803832174,
      "W": 0.939064016845464
    },
    "Touch": [
      {
        "X": 0.3866666555404663,
        "Y": -0.03598201274871826,
        "Radius": 38.47137451171875,
        "Force": 2.5
      },
      {
        "X": -0.40533334016799927,
        "Y": 0.2353823184967041,
        "Radius": 38.47137451171875,
        "Force": 3.8499999046325684
      }
    ],
    "Compass": {
      "Compass": 214.3119659423828,
      "Faceup": 1,
      "IsFaceup": true
    },
    "GPS": {
      "Latitude": 11.11111111111,
      "Longitude": 22.22222222222
    },
    "Light": {
      "Light": 43
    },
    "ProximityMonitor": {
      "ProximityMonitor": false
    },
    "Pressure": {
      "Altitude": 1.9341773986816406,
      "Pressure": 1015.1840209960938
    },
    "MicLevel": {
      "Max": -37.1226921081543,
      "Average": -40.96709060668945
    }
  }
}
```

## TODO

- [ ] Support for OSC format
- [ ] Support for all data attributes. (Currently limited to implementation of only those attributes that could be tested)
