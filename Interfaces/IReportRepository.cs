using WorkTimeTracker.Dtos.Report;
using WorkTimeTracker.Helpers;
using WorkTimeTracker.Models;

namespace WorkTimeTracker.Interfaces
{
    public interface IReportRepository
    {
        Task<Employee?> GetAsync(int employeeId, ReportRequestDto reportDto);
        Task<List<Employee>> GetAllAsync(EmployeeQueryObject query, ReportRequestDto reportDto);
    }
}