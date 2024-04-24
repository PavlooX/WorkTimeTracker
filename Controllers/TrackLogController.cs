using Microsoft.AspNetCore.Mvc;
using WorkTimeTracker.Dtos.TrackLog;
using WorkTimeTracker.Interfaces;
using WorkTimeTracker.Mappers;

namespace WorkTimeTracker.Controllers
{
    [ApiController]
    [Route("api/tracklog")]
    public class TrackLogController(ITrackLogRepository trackLogRepo) : ControllerBase
    {
        private readonly ITrackLogRepository _trackLogRepo = trackLogRepo;

        [HttpPost]
        [Route("start/{employeeId:int}")]
        public async Task<IActionResult> StartAsync([FromRoute] int employeeId, [FromBody] StartTrackLogRequestDto trackLogDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trackLogNew = trackLogDto.FromStartDtoToTrackLog(employeeId);

            var trackLog = await _trackLogRepo.StartAsync(trackLogNew);
            if (trackLog == null)
            {
                return NotFound();
            }

            return Ok(trackLog.FromTrackLogToTrackLogDto());
        }

        [HttpPut]
        [Route("end/{employeeId:int}")]
        public async Task<IActionResult> EndAsync([FromRoute] int employeeId, [FromBody] EndTrackLogRequestDto trackLogDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trackLog = await _trackLogRepo.EndAsync(employeeId, trackLogDto);
            if (trackLog == null)
            {
                return NotFound();
            }

            return Ok(trackLog.FromTrackLogToTrackLogDto());
        }
    }
}
