using FluentValidation;
using StudentCourseRegistrationAPI.Models;

namespace StudentCourseRegistrationAPI.Validator
{
    public class StudentRegistrationValidator : AbstractValidator<StudentRegistration>
    {
        public StudentRegistrationValidator()
        {
            RuleFor(x => x.StudentName)
                .NotEmpty()
                .MinimumLength(3)
                .WithMessage("StudentName must contain minimum 3 characters.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Email must be in a valid email format.");

            RuleFor(x => x.CourseCode)
                .NotEmpty()
                .Length(6)
                .WithMessage("CourseCode must be exactly 6 characters.");

            RuleFor(x => x.Semester)
                .InclusiveBetween(1, 8)
                .WithMessage("Semester must be between 1 and 8.");

            RuleFor(x => x.RegistrationDate)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("RegistrationDate must not be a future date.");
        }
    }
}
