using WorkTimeTracker.Dtos.Employee;
using WorkTimeTracker.Models;

namespace WorkTimeTracker.Mappers
{
    public static class EmployeeMapper
    {
        public static EmployeeDto FromEmployeeToEmployeeDto(this Employee employee)
        {
            var employeeDto = new EmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Created = employee.Created,
                LastUpdate = employee.LastUpdate,
                TrackLogs = employee.TrackLogs.Select(t => t.FromTrackLogToTrackLogDto()).ToList()
            };

            // set tracklogs count if count is more than zero
            employeeDto.TrackLogsCount = (employeeDto.TrackLogs.Count > 0) ? employeeDto.TrackLogs.Count : null;

            // order tracklogs from newest to oldest
            employeeDto.TrackLogs = employeeDto.TrackLogs.OrderByDescending(t => t.Id).ToList();

            // calculate working hours
            foreach (var t in employeeDto.TrackLogs)
            {
                employeeDto.WorkingHours += t.Duration;
            }

            return employeeDto;
        }

        public static Employee FromCreateDtoToEmployee(this CreateEmployeeRequestDto employeeDto)
        {
            return new Employee
            {
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName
            };
        }
    }
}