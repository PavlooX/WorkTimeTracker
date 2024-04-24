using System.ComponentModel.DataAnnotations;

namespace WorkTimeTracker.Helpers
{
    public class EmployeeQueryObject
    {
        public enum SortByOptions
        {
            None,
            Id,
            FirstName,
            LastName,
            WorkingHours
        }
        public enum SortOrderOptions
        {
            None,
            Ascending,
            Descending
        }

        [MaxLength(100, ErrorMessage = "Search cannot be over 100 characters")]
        public string? Search { get; set; }
        public SortByOptions SortBy { get; set; }
        public SortOrderOptions SortOrder { get; set; }
    }
}