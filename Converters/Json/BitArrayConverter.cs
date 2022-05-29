using System;
using System.Collections;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Processors.Converters.Json
{
    public class BitArrayConverter : JsonConverter<BitArray>
    {
        class BitArrayDTO
        {
            public byte[] Bytes { get; set; }
            public int Length { get; set; }
        }

        public override BitArray Read(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions options)
        {
            BitArrayDTO dto = JsonSerializer.Deserialize<BitArrayDTO>(ref reader);

            if (dto is null)
                return null;

            var bitArray = new BitArray(dto.Bytes);
            bitArray.Length = dto.Length;

            return bitArray;
        }

        public override void Write(Utf8JsonWriter writer, BitArray value, JsonSerializerOptions options)
        {
            byte[] serializable = new byte[(int)Math.Ceiling(value.Length / 8.0)];
            value.CopyTo(serializable, 0);

            BitArrayDTO dto = new BitArrayDTO
            {
                Bytes = serializable,
                Length = value.Length
            };
            
            JsonSerializer.Serialize(writer, dto);
        }
    }
}