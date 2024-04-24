using WorkTimeTracker.Dtos.TrackLog;
using WorkTimeTracker.Models;

namespace WorkTimeTracker.Mappers
{
    public static class TrackLogMapper
    {
        public static TrackLogDto FromTrackLogToTrackLogDto(this TrackLog trackLog)
        {
            var trackLogDto = new TrackLogDto
            {
                Id = trackLog.Id,
                StartTime = trackLog.StartTime,
                EndTime = trackLog.EndTime,
                EmployeeId = trackLog.EmployeeId
            };

            // calculate duration if endtime is set
            if (trackLogDto.EndTime != null)
            {
                trackLogDto.Duration = (TimeSpan)(trackLogDto.EndTime - trackLogDto.StartTime);
            }

            return trackLogDto;
        }

        public static TrackLog FromStartDtoToTrackLog(this StartTrackLogRequestDto trackLogDto, int employeeId)
        {
            return new TrackLog
            {
                EmployeeId = employeeId,
                StartTime = trackLogDto.StartTime
            };
        }
    }
}