using Newtonsoft.Json;

namespace WorkTimeTracker.Helpers
{
    public class TimeSpanConverter : JsonConverter<TimeSpan>
    {
        public override TimeSpan ReadJson(JsonReader reader, Type objectType, TimeSpan existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, TimeSpan value, JsonSerializer serializer)
        {
            if (value == TimeSpan.Zero)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteValue($"{(int)value.TotalHours:00}h : {value.Minutes:00}m : {value.Seconds:00}s");
            }
        }
    }
}