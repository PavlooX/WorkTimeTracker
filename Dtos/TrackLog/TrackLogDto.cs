using Newtonsoft.Json;
using WorkTimeTracker.Helpers;

namespace WorkTimeTracker.Dtos.TrackLog
{
    public class TrackLogDto
    {
        public int Id { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime StartTime { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? EndTime { get; set; }

        [JsonConverter(typeof(TimeSpanConverter))]
        public TimeSpan Duration { get; set; } = TimeSpan.Zero;

        public int EmployeeId { get; set; }
    }
}