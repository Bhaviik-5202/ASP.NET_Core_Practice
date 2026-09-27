using EmployeeManagementAPI.DTOs;
using FluentValidation;

namespace EmployeeManagementAPI.Validators
{
    public class EmployeeValidator : AbstractValidator<EmployeeCreateDto>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.EmployeeName)
                .NotEmpty()
                .Length(3, 100)
                .Matches(@"^[a-zA-Z\s\-'.]+$")
                .Must(x => x == x.Trim())
                .WithMessage("Employee name is invalid.");

            RuleFor(x => x.EmailAddress)
                .NotEmpty()
                .EmailAddress()
                .Must(x => !x.Any(char.IsWhiteSpace))
                .WithMessage("Email address is invalid.");

            RuleFor(x => x.MobileNumber)
                .NotEmpty()
                .Matches(@"^\d+$")
                .WithMessage("Mobile number must contain only digits.");

            RuleFor(x => x.EmployeeCode)
                .MaximumLength(100)
                .WithMessage("Employee code must not exceed 100 characters.");

            RuleFor(x => x.Salary)
                .GreaterThan(0)
                .LessThanOrEqualTo(1000000)
                .WithMessage("Salary must be between 1 and 10,00,000.");

            RuleFor(x => x.JoiningDate)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("Joining date must not be a future date.");

            RuleFor(x => x.BirthDate)
                .NotEmpty()
                .LessThan(DateTime.Now)
                .WithMessage("Birth date must be in the past.");

            RuleFor(x => x.IsActive)
                .NotNull()
                .WithMessage("IsActive must contain a valid boolean value.");
        }
    }

    public class EmployeeUpdateValidator : AbstractValidator<EmployeeUpdateDto>
    {
        public EmployeeUpdateValidator()
        {
            RuleFor(x => x.EmployeeName)
                .NotEmpty()
                .Length(3, 100)
                .Matches(@"^[a-zA-Z\s\-'.]+$")
                .Must(x => x == x.Trim())
                .WithMessage("Employee name is invalid.");

            RuleFor(x => x.EmailAddress)
                .NotEmpty()
                .EmailAddress()
                .Must(x => !x.Any(char.IsWhiteSpace))
                .WithMessage("Email address is invalid.");

            RuleFor(x => x.MobileNumber)
                .NotEmpty()
                .Matches(@"^\d+$")
                .WithMessage("Mobile number must contain only digits.");

            RuleFor(x => x.EmployeeCode)
                .MaximumLength(100)
                .WithMessage("Employee code must not exceed 100 characters.");

            RuleFor(x => x.Salary)
                .GreaterThan(0)
                .LessThanOrEqualTo(1000000)
                .WithMessage("Salary must be between 1 and 10,00,000.");

            RuleFor(x => x.JoiningDate)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("Joining date must not be a future date.");

            RuleFor(x => x.BirthDate)
                .NotEmpty()
                .LessThan(DateTime.Now)
                .WithMessage("Birth date must be in the past.");

            RuleFor(x => x.IsActive)
                .NotNull()
                .WithMessage("IsActive must contain a valid boolean value.");
        }
    }
}
