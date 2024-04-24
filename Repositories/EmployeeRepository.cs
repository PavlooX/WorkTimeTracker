using Microsoft.EntityFrameworkCore;
using WorkTimeTracker.Data;
using WorkTimeTracker.Dtos.Employee;
using WorkTimeTracker.Helpers;
using WorkTimeTracker.Interfaces;
using WorkTimeTracker.Models;

namespace WorkTimeTracker.Repositories
{
    public class EmployeeRepository(AppDbContext context) : IEmployeeRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Employee> CreateAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);

            await _context.SaveChangesAsync();

            return employee;
        }

        public async Task<Employee?> GetAsync(int employeeId)
        {
            return await _context.Employees.Include(e => e.TrackLogs).FirstOrDefaultAsync(e => e.Id == employeeId);
        }

        public async Task<List<Employee>> GetAllAsync(EmployeeQueryObject query)
        {
            var employees = _context.Employees.Include(e => e.TrackLogs).AsQueryable();

            // if search is set - search by first name or last name
            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                employees = employees.Where(e =>
                    e.FirstName.ToLower().Contains(query.Search.ToLower()) ||
                    e.LastName.ToLower().Contains(query.Search.ToLower()));
            }            

            return await employees.ToListAsync();
        }

        public async Task<Employee?> UpdateAsync(int employeeId, UpdateEmployeeRequestDto employeeDto)
        {
            // check if employee with given id exists
            var employee = await GetAsync(employeeId);
            if (employee == null)
            {
                return null;
            }

            await employee.UpdateAsync(employeeDto);
            await employee.ChangeLastUpdateAsync(DateTime.Now);

            await _context.SaveChangesAsync();

            return employee;
        }

        public async Task<bool> DeleteAsync(int employeeId)
        {
            // check if employee with given id exists
            var employee = await GetAsync(employeeId);
            if (employee == null)
            {
                return false;
            }

            _context.Employees.Remove(employee);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}