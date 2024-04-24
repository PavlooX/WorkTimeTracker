using System.ComponentModel.DataAnnotations;

namespace WorkTimeTracker.Dtos.TrackLog
{
    public class StartTrackLogRequestDto
    {
        [Required]
        public DateTime StartTime { get; set; }
    }
}