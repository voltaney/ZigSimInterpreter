using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

using Xunit.Abstractions;

using ZigSimInterpreter.Models;

namespace ZigSimInterpreter.Test
{
    internal class TestDataGenerator
    {
        // Full example values for DeviceInfo
        static readonly DeviceInfo FullDeviceInfo = new(
            OS: "OS Name",
            OSVersion: "1.0.0",
            Name: "Device Name",
            UUID: "UUID example",
            DisplayWidth: 1200,
            DisplayHeight: 1800
        );
        // Full example values for SensorData
        static readonly SensorData FullSensorData = new(
            Accel: new(1.0, 2.0, 3.0),
            Gyro: new(4.0, 5.0, 6.0),
            Gravity: new(7.0, 8.0, 9.0),
            Quaternion: new(10.0, 11.0, 12.0, 13.0),
            Touch: [new(1.0, 2.0, null, null), new(4.0, 5.0, 6.0, 7.0)],
            Compass: new(1.0, 1),
            GPS: new(1.0, 2.0),
            Light: new(1.0),
            ProximityMonitor: new(true),
            Pressure: new(12.0, 14.0),
            MicLevel: new(19.0, 20.0)
        );

        // Generate payloads with full values
        public static IEnumerable<object[]> FullPayloads()
        {
            yield return new object[]
            {
                new ZigSimPayload(FullDeviceInfo, "2021-01-01T00:00:00Z", FullSensorData)
            };
        }

        // Generate payloads with null values
        public static IEnumerable<object[]> NullPayloads()
        {
            yield return new object[]
            {
                new ZigSimPayload()
            };
            yield return new object[]
            {
                new ZigSimPayload(FullDeviceInfo)
            };
            yield return new object[]
            {
                new ZigSimPayload(FullDeviceInfo, null, new SensorData())
            };
        }

        // Generate payloads with different values
        public static IEnumerable<object[]> DifferenctPayloads()
        {
            var payload1 = new ZigSimPayload(FullDeviceInfo, "2021-01-01T00:00:00Z", FullSensorData);
            yield return new object[]
            {
                payload1,
                payload1 with {
                    SensorData = FullSensorData with
                    {
                        Touch = [new(10, 20, null, null)]
                    }
                }
            };
            yield return new object[]
            {
                payload1,
                payload1 with {
                    SensorData = FullSensorData with
                    {
                        Light = new(-10)
                    }
                }
            };
        }

        // Generate payloads with same values
        public static IEnumerable<object[]> SamePayloads()
        {
            var payload1 = new ZigSimPayload(FullDeviceInfo, "2021-01-01T00:00:00Z", FullSensorData);
            // same reference
            yield return new object[] { payload1, payload1 };
            // same value
            yield return new object[] { payload1, payload1 with { } };
        }
    }

    // Inject ITestOutputHelper to write logs
    public class ZigSimJsonInterpreterTests(ITestOutputHelper output)
    {
        [Theory]
        [MemberData(nameof(TestDataGenerator.FullPayloads), MemberType = typeof(TestDataGenerator))]
        public void Read_ValidFullJson_ReturnsSuccessResult(ZigSimPayload payload)
        {
            // Log
            output.WriteLine(JsonSerializer.Serialize(payload));

            // Arrange
            var interpreter = new ZigSimJsonInterpreter();
            var json = JsonSerializer.Serialize(payload);
            var input = Encoding.UTF8.GetBytes(json);

            // Act
            var result = interpreter.Read(input);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Payload);
            Assert.Equal(payload, result.Payload);
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.NullPayloads), MemberType = typeof(TestDataGenerator))]
        public void Read_ValidNullJson_ReturnsSuccessResult(ZigSimPayload payload)
        {
            // Log
            output.WriteLine(JsonSerializer.Serialize(payload));

            // Arrange
            var interpreter = new ZigSimJsonInterpreter();
            var json = JsonSerializer.Serialize(payload, new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
            var input = Encoding.UTF8.GetBytes(json);

            // Act
            var result = interpreter.Read(input);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Payload);
            Assert.Equal(payload, result.Payload);
        }

        [Theory]
        [InlineData("invaid json")] // no closing brace
        [InlineData("{")] // missing closing brace
        public void Read_InvalidJson_ReturnsFailureResult(string invalidJson)
        {
            // Log
            output.WriteLine(invalidJson);

            // Arrange
            var interpreter = new ZigSimJsonInterpreter();
            var input = Encoding.UTF8.GetBytes(invalidJson);

            // Act
            var result = interpreter.Read(input);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Null(result.Payload);
            Assert.NotNull(result.ErrorMessage);
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.DifferenctPayloads), MemberType = typeof(TestDataGenerator))]
        public void Equals_DifferentPayloads_ReturnsFalse(ZigSimPayload payload1, ZigSimPayload payload2)
        {
            // Act
            var result = payload1.Equals(payload2);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.SamePayloads), MemberType = typeof(TestDataGenerator))]
        public void Equals_SamePayloads_ReturnsTrue(ZigSimPayload payload1, ZigSimPayload payload2)
        {
            // Act
            var result = payload1.Equals(payload2);

            // Assert
            Assert.True(result);
        }
    }
}
