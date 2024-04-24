using Newtonsoft.Json;
using WorkTimeTracker.Dtos.TrackLog;
using WorkTimeTracker.Helpers;

namespace WorkTimeTracker.Dtos.Report
{
    public class ReportDto
    {
        public int Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        [JsonConverter(typeof(TimeSpanConverter))]
        public TimeSpan WorkingHours { get; set; } = TimeSpan.Zero;

        public int? TrackLogsCount { get; set; }

        public List<TrackLogDto> TrackLogs { get; set; } = [];
    }
}