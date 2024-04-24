using WorkTimeTracker.Dtos.TrackLog;
using WorkTimeTracker.Models;

namespace WorkTimeTracker.Interfaces
{
    public interface ITrackLogRepository
    {
        Task<TrackLog?> GetActiveAsync(int employeeId);
        Task<TrackLog?> GetLatestAsync(int employeeId);
        Task<TrackLog?> StartAsync(TrackLog trackLog);
        Task<TrackLog?> EndAsync(int employeeId, EndTrackLogRequestDto trackLogDto);
    }
}