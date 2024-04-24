using Microsoft.AspNetCore.Mvc;
using WorkTimeTracker.Dtos.Report;
using WorkTimeTracker.Helpers;
using WorkTimeTracker.Interfaces;
using WorkTimeTracker.Mappers;

namespace WorkTimeTracker.Controllers
{
    [ApiController]
    [Route("api/report")]
    public class ReportController(IReportRepository reportRepo) : ControllerBase
    {
        private readonly IReportRepository _reportRepo = reportRepo;

        [HttpPost]
        [Route("{employeeId:int}")]
        public async Task<IActionResult> GetAsync([FromRoute] int employeeId, [FromBody] ReportRequestDto reportDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = await _reportRepo.GetAsync(employeeId, reportDto);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee.FromEmployeeToReportDto());
        }

        [HttpPost]
        [Route("all")]
        public async Task<IActionResult> GetAllAsync([FromQuery] EmployeeQueryObject query, [FromBody] ReportRequestDto reportDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employees = await _reportRepo.GetAllAsync(query, reportDto);

            var reportsDto = employees.Select(e => e.FromEmployeeToReportDto()).AsQueryable();

            // sort order (asc, desc)
            bool desc = (query.SortOrder == EmployeeQueryObject.SortOrderOptions.Descending) ? true : false;

            // sort by (id, firstname, lastname, workinghours)
            // default by workinghours desc
            switch (query.SortBy)
            {
                case EmployeeQueryObject.SortByOptions.Id:
                    reportsDto = desc ? reportsDto.OrderByDescending(r => r.Id) : reportsDto.OrderBy(r => r.Id);
                    break;
                case EmployeeQueryObject.SortByOptions.FirstName:
                    reportsDto = desc ? reportsDto.OrderByDescending(r => r.FirstName) : reportsDto.OrderBy(r => r.FirstName);
                    break;
                case EmployeeQueryObject.SortByOptions.LastName:
                    reportsDto = desc ? reportsDto.OrderByDescending(r => r.LastName) : reportsDto.OrderBy(r => r.LastName);
                    break;
                case EmployeeQueryObject.SortByOptions.WorkingHours:
                    reportsDto = desc ? reportsDto.OrderByDescending(r => r.WorkingHours) : reportsDto.OrderBy(r => r.WorkingHours);
                    break;
                default:
                    reportsDto = reportsDto.OrderByDescending(r => r.WorkingHours);
                    break;
            }

            return Ok(reportsDto.ToList());
        }
    }
}