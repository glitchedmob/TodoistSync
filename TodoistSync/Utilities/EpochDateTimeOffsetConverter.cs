using System;
using Newtonsoft.Json;

namespace TodoistSync.Utilities
{
    public class EpochDateTimeOffsetConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
            {
                return null;
            }

            var epoch = long.Parse((string)reader.Value);
            return DateTimeOffset.FromUnixTimeMilliseconds(epoch);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTimeOffset?);
        }
    }
}
