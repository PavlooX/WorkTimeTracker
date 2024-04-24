using Microsoft.AspNetCore.Mvc;
using WorkTimeTracker.Dtos.Employee;
using WorkTimeTracker.Helpers;
using WorkTimeTracker.Interfaces;
using WorkTimeTracker.Mappers;

namespace WorkTimeTracker.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController(IEmployeeRepository employeeRepo) : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepo = employeeRepo;

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateEmployeeRequestDto employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeeNew = employeeDto.FromCreateDtoToEmployee();

            var employee = await _employeeRepo.CreateAsync(employeeNew);

            return Ok(employee.FromEmployeeToEmployeeDto());
        }

        [HttpGet]
        [Route("{employeeId:int}")]
        public async Task<IActionResult> GetAsync([FromRoute] int employeeId)
        {
            var employee = await _employeeRepo.GetAsync(employeeId);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee.FromEmployeeToEmployeeDto());
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllAsync([FromQuery] EmployeeQueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employees = await _employeeRepo.GetAllAsync(query);

            var employeesDto = employees.Select(e => e.FromEmployeeToEmployeeDto()).AsQueryable();

            // sort order (asc, desc)
            bool desc = (query.SortOrder == EmployeeQueryObject.SortOrderOptions.Descending) ? true : false;

            // sort by (id, firstname, lastname, workinghours)
            // default by id
            switch (query.SortBy)
            {
                case EmployeeQueryObject.SortByOptions.Id:
                    employeesDto = desc ? employeesDto.OrderByDescending(e => e.Id) : employeesDto.OrderBy(e => e.Id);
                    break;
                case EmployeeQueryObject.SortByOptions.FirstName:
                    employeesDto = desc ? employeesDto.OrderByDescending(e => e.FirstName) : employeesDto.OrderBy(e => e.FirstName);
                    break;
                case EmployeeQueryObject.SortByOptions.LastName:
                    employeesDto = desc ? employeesDto.OrderByDescending(e => e.LastName) : employeesDto.OrderBy(e => e.LastName);
                    break;
                case EmployeeQueryObject.SortByOptions.WorkingHours:
                    employeesDto = desc ? employeesDto.OrderByDescending(e => e.WorkingHours) : employeesDto.OrderBy(e => e.WorkingHours);
                    break;
                default:
                    employeesDto = desc ? employeesDto.OrderByDescending(e => e.Id) : employeesDto.OrderBy(e => e.Id);
                    break;
            }

            return Ok(employeesDto.ToList());
        }

        [HttpPut]
        [Route("update/{employeeId:int}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int employeeId, [FromBody] UpdateEmployeeRequestDto employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = await _employeeRepo.UpdateAsync(employeeId, employeeDto);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee.FromEmployeeToEmployeeDto());
        }

        [HttpDelete]
        [Route("delete/{employeeId:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int employeeId)
        {
            bool deleteStatus = await _employeeRepo.DeleteAsync(employeeId);
            if (!deleteStatus)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}