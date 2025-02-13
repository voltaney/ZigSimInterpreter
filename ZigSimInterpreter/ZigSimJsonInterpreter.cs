using System.Text;
using System.Text.Json;

using ZigSimInterpreter.Models;

namespace ZigSimInterpreter
{
    public class ZigSimJsonInterpreter : IZigSimInterpreter
    {
        private readonly JsonSerializerOptions _options;
        public ZigSimJsonInterpreter()
        {
            _options = new JsonSerializerOptions();
            _options.PropertyNameCaseInsensitive = true;
        }

        /// <summary>
        /// Interpret the input byte[] as a ZigSimPayload.
        /// If JsonException is thrown, return a ZigSimResult with IsSuccess = false and the exception message.
        /// </summary>
        /// <param name="input">data to interpret</param>
        /// <returns>ZigSimResult containing the deserialized ZigSimPayload or an error message</returns>
        public ZigSimResult Read(byte[] input)
        {
            // convert byte[] to string with UTF-8 encoding
            string data = Encoding.UTF8.GetString(input);
            try
            {
                // deserialize the string data to ZigSimPayload
                return new ZigSimResult(JsonSerializer.Deserialize<ZigSimPayload>(input, _options), true);
            }
            catch (JsonException ex)
            {
                // return a ZigSimResult with the error message if deserialization fails
                return new ZigSimResult(null, false, ex.Message);
            }
        }
    }
}
