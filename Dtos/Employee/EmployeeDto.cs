using Newtonsoft.Json;
using WorkTimeTracker.Dtos.TrackLog;
using WorkTimeTracker.Helpers;

namespace WorkTimeTracker.Dtos.Employee
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Created { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? LastUpdate { get; set; }

        [JsonConverter(typeof(TimeSpanConverter))]
        public TimeSpan WorkingHours { get; set; } = TimeSpan.Zero;

        public int? TrackLogsCount { get; set; }

        public List<TrackLogDto> TrackLogs { get; set; } = [];
    }
}