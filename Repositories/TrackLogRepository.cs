using Microsoft.EntityFrameworkCore;
using WorkTimeTracker.Data;
using WorkTimeTracker.Dtos.TrackLog;
using WorkTimeTracker.Interfaces;
using WorkTimeTracker.Models;

namespace WorkTimeTracker.Repositories
{
    public class TrackLogRepository(AppDbContext context, IEmployeeRepository employeeRepo) : ITrackLogRepository
    {
        private readonly AppDbContext _context = context;
        private readonly IEmployeeRepository _employeeRepo = employeeRepo;

        public async Task<TrackLog?> GetActiveAsync(int employeeId)
        {
            return await _context.TrackLogs.FirstOrDefaultAsync(t => 
                t.EmployeeId == employeeId && 
                t.EndTime == null);
        }

        public async Task<TrackLog?> GetLatestAsync(int employeeId)
        {
            return await _context.TrackLogs
                .Where(t => t.EmployeeId == employeeId)
                .OrderByDescending(t => t.EndTime)
                .FirstOrDefaultAsync();
        }

        public async Task<TrackLog?> StartAsync(TrackLog trackLog)
        {
            // check if employee with given id exists
            var employee = await _employeeRepo.GetAsync(trackLog.EmployeeId);
            if (employee == null)
            {
                return null;
            }

            // check if an active tracklog already exists
            var activeTrackLog = await GetActiveAsync(trackLog.EmployeeId);
            if (activeTrackLog != null)
            {
                return null;
            }

            // check if new starttime is later than the latest tracklog endtime
            var latestTrackLog = await GetLatestAsync(trackLog.EmployeeId);
            if (latestTrackLog != null)
            {
                if (latestTrackLog.EndTime >= trackLog.StartTime)
                {
                    return null;
                }
            }

            await _context.TrackLogs.AddAsync(trackLog);

            await employee.ChangeLastUpdateAsync(trackLog.StartTime);

            await _context.SaveChangesAsync();

            return trackLog;
        }

        public async Task<TrackLog?> EndAsync(int employeeId, EndTrackLogRequestDto trackLogDto)
        {
            // check if employee with given id exists
            var employee = await _employeeRepo.GetAsync(employeeId);
            if (employee == null)
            {
                return null;
            }

            // check if there is an active tracklog
            var activeTrackLog = await GetActiveAsync(employeeId);
            if (activeTrackLog == null)
            {
                return null;
            }

            // check if endtime is later than starttime
            if (activeTrackLog.StartTime >= trackLogDto.EndTime)
            {
                return null;
            }            

            await activeTrackLog.EndAsync(trackLogDto.EndTime);

            await employee.ChangeLastUpdateAsync(trackLogDto.EndTime);

            await _context.SaveChangesAsync();

            return activeTrackLog;
        }
    }
}