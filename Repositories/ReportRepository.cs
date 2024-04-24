using Microsoft.EntityFrameworkCore;
using WorkTimeTracker.Data;
using WorkTimeTracker.Dtos.Report;
using WorkTimeTracker.Helpers;
using WorkTimeTracker.Interfaces;
using WorkTimeTracker.Models;

namespace WorkTimeTracker.Repositories
{
    public class ReportRepository(AppDbContext context) : IReportRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Employee?> GetAsync(int employeeId, ReportRequestDto reportDto)
        {
            var employee = await _context.Employees
                .Include(e => e.TrackLogs)
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee == null)
            {
                return null;
            }

            // show only tracklogs that fall within the specified time range
            employee.TrackLogs = employee.TrackLogs.Where(t =>
                t.StartTime >= reportDto.FromTime &&
                t.EndTime <= reportDto.ToTime).ToList();      

            return employee;
        }

        public async Task<List<Employee>> GetAllAsync(EmployeeQueryObject query, ReportRequestDto reportDto)
        {
            var employees = _context.Employees.Include(e => e.TrackLogs).AsQueryable();

            /* *optional* filter employees based on time range [FromTime, ToTime]
            employees = employees.Where(e => e.TrackLogs.Any(t =>
                t.StartTime >= reportDto.FromTime &&
                t.EndTime <= reportDto.ToTime)); */

            // if search is set - search by first name or last name
            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                employees = employees.Where(e =>
                    e.FirstName.ToLower().Contains(query.Search.ToLower()) ||
                    e.LastName.ToLower().Contains(query.Search.ToLower()));
            }

            // for every user show only tracklogs that fall within the specified time range
            employees = employees.Select(e => new Employee
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Created = e.Created,
                LastUpdate = e.LastUpdate,
                TrackLogs = e.TrackLogs.Where(t =>
                    t.StartTime >= reportDto.FromTime &&
                    t.EndTime <= reportDto.ToTime).ToList()
            });

            return await employees.ToListAsync();
        }
    }
}
