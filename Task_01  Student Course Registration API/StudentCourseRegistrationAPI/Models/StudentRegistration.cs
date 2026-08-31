using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentCourseRegistrationAPI.Models
{
    public class StudentRegistration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }

        public string StudentName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string CourseCode { get; set; } = string.Empty;

        public int Semester { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}
