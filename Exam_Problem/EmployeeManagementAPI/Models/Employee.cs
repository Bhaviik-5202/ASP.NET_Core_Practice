namespace EmployeeManagementAPI.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; } = string.Empty;

        public string EmailAddress { get; set; } = string.Empty;

        public string MobileNumber { get; set; } = string.Empty;

        public string? EmployeeCode { get; set; }

        public decimal Salary { get; set; }

        public DateTime JoiningDate { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsActive { get; set; }
    }
}
