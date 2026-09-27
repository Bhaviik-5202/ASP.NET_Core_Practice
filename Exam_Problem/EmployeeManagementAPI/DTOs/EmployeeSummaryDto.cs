namespace EmployeeManagementAPI.DTOs
{
    public class EmployeeSummaryDto
    {
        public int TotalEmployees { get; set; }

        public int ActiveEmployees { get; set; }

        public int InactiveEmployees { get; set; }

        public decimal AverageSalary { get; set; }
    }
}
