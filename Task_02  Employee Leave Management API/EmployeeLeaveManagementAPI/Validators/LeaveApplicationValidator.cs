using EmployeeLeaveManagementAPI.DTOs;
using FluentValidation;

namespace EmployeeLeaveManagementAPI.Validators
{
    public class LeaveApplicationValidator : AbstractValidator<LeaveApplicationDto>
    {
        public LeaveApplicationValidator()
        {
            RuleFor(x => x.EmployeeName)
                .NotEmpty()
                .WithMessage("EmployeeName is required.");

            RuleFor(x => x.LeaveType)
                .Must(x => x == "Casual" || x == "Sick" || x == "Earned")
                .WithMessage("LeaveType must be Casual, Sick, or Earned.");

            RuleFor(x => x.FromDate)
                .LessThan(x => x.ToDate)
                .WithMessage("FromDate must be earlier than ToDate.");

            RuleFor(x => x.ToDate)
                .LessThanOrEqualTo(x => x.FromDate.AddDays(10))
                .WithMessage("Total leave duration must not exceed 10 days.");

            RuleFor(x => x.Reason)
                .MinimumLength(10)
                .WithMessage("Reason must contain at least 10 characters.");
        }
    }
}
