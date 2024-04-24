using WorkTimeTracker.Dtos.Report;
using WorkTimeTracker.Models;

namespace WorkTimeTracker.Mappers
{
    public static class ReportMapper
    {
        public static ReportDto FromEmployeeToReportDto(this Employee employee)
        {
            var reportDto = new ReportDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                TrackLogs = employee.TrackLogs.Select(t => t.FromTrackLogToTrackLogDto()).ToList()
            };

            // set tracklogs count if count is more than zero
            reportDto.TrackLogsCount = (reportDto.TrackLogs.Count > 0) ? reportDto.TrackLogs.Count : null;

            // order tracklogs from newest to oldest
            reportDto.TrackLogs = reportDto.TrackLogs.OrderByDescending(t => t.Id).ToList();

            // calculate working hours
            foreach (var t in reportDto.TrackLogs)
            {
                reportDto.WorkingHours += t.Duration;
            }

            return reportDto;
        }
    }
}