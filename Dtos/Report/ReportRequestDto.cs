using System.ComponentModel.DataAnnotations;

namespace WorkTimeTracker.Dtos.Report
{
    public class ReportRequestDto
    {
        [Required]
        public DateTime FromTime { get; set; }

        [Required]
        public DateTime ToTime { get; set; }
    }
}