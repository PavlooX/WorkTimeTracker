using WorkTimeTracker.Dtos.Employee;

namespace WorkTimeTracker.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? LastUpdate { get; set; }
        public List<TrackLog> TrackLogs { get; set; } = [];

        public async Task UpdateAsync(UpdateEmployeeRequestDto employeeDto)
        {
            await Task.Run(() =>
            {
                FirstName = employeeDto.FirstName;
                LastName = employeeDto.LastName;
            });
        }
        public async Task ChangeLastUpdateAsync(DateTime time)
        {
            await Task.Run(() =>
            {
                LastUpdate = time;
            });
        }
    }
}