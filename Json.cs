using System.IO;
using System.Text.Json;

namespace Processors
{
    public class Json : Serializer
    {
        public Json()
        {
            _options = new JsonSerializerOptions();
        }

        public Json(JsonSerializerOptions options)
        {
            _options = options;
        }

        private protected override T Deserialize<T>(string path)
        {
            byte[] fileBytes = File.ReadAllBytes(path);
            Utf8JsonReader utf8Reader = new Utf8JsonReader(fileBytes);
            return JsonSerializer.Deserialize<T>(ref utf8Reader, _options);
        }

        private protected override void Serialize<T>(T serializable, string path)
        {
            byte[] jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(serializable, _options);
            File.WriteAllBytes(path, jsonUtf8Bytes);
        }

        private JsonSerializerOptions _options;
    }
}