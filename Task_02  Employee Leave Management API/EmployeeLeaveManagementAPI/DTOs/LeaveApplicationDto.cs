namespace EmployeeLeaveManagementAPI.DTOs
{
    public class LeaveApplicationDto
    {
        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; } = string.Empty;

        public string LeaveType { get; set; } = string.Empty;

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string Reason { get; set; } = string.Empty;
    }
}
