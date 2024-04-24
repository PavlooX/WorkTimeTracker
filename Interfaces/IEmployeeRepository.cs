using WorkTimeTracker.Dtos.Employee;
using WorkTimeTracker.Helpers;
using WorkTimeTracker.Models;

namespace WorkTimeTracker.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> CreateAsync(Employee employee);
        Task<Employee?> GetAsync(int employeeId);
        Task<List<Employee>> GetAllAsync(EmployeeQueryObject query);
        Task<Employee?> UpdateAsync(int employeeId, UpdateEmployeeRequestDto employeeDto);
        Task<bool> DeleteAsync(int employeeId);
    }
}