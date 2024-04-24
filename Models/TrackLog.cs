namespace WorkTimeTracker.Models
{
    public class TrackLog
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public async Task EndAsync(DateTime endTime)
        {
            await Task.Run(() =>
            {
                EndTime = endTime;
            });
        }
    }
}