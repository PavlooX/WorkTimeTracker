using Newtonsoft.Json;

namespace WorkTimeTracker.Helpers
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
        {
            var formattedDate = value.ToString("yyyy-MM-dd HH:mm:ss");
            writer.WriteValue(formattedDate);
        }
    }
}