using System.ComponentModel.DataAnnotations;

namespace WorkTimeTracker.Dtos.Employee
{
    public class UpdateEmployeeRequestDto
    {
        [Required]
        [MinLength(1, ErrorMessage = "First name must be at least 1 character")]
        [MaxLength(50, ErrorMessage = "First name cannot be over 50 characters")]
        public required string FirstName { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Last name must be at least 1 character")]
        [MaxLength(50, ErrorMessage = "Last name cannot be over 50 characters")]
        public required string LastName { get; set; }
    }
}