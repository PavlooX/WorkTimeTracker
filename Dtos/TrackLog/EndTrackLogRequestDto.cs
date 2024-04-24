using System.ComponentModel.DataAnnotations;

namespace WorkTimeTracker.Dtos.TrackLog
{
    public class EndTrackLogRequestDto
    {
        [Required]
        public DateTime EndTime { get; set; }
    }
}