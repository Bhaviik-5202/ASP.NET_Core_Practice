using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentCourseRegistrationAPI.Data;
using StudentCourseRegistrationAPI.Models;

namespace StudentCourseRegistrationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationsController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public RegistrationsController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterStudent(
            StudentRegistration registration,
            [FromServices] IValidator<StudentRegistration> validator)
        {
            var result = await validator.ValidateAsync(registration);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            _appDbContext.StudentRegistrations.Add(registration);

            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetRegistration),
                new { id = registration.StudentId },
                registration);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegistrations()
        {
            var registrations = await _appDbContext.StudentRegistrations.ToListAsync();
            return Ok(registrations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegistration(int id)
        {
            var registration = await _appDbContext.StudentRegistrations.FindAsync(id);
            if (registration == null)
            {
                return NotFound(new { message = $"Student registration with ID {id} was not found." });
            }

            return Ok(registration);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRegistration(
            int id,
            [FromBody] StudentRegistration registration)
        {
            registration.StudentId = id;

            var existing = await _appDbContext.StudentRegistrations
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.StudentId == id);

            if (existing == null)
            {
                return NotFound(new { message = "Student registration not found." });
            }

            _appDbContext.StudentRegistrations.Update(registration);

            await _appDbContext.SaveChangesAsync();

            return Ok(registration);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistration(int id)
        {
            var registration = await _appDbContext.StudentRegistrations.FindAsync(id);
            if (registration == null)
            {
                return NotFound(new { message = $"Student registration with ID {id} was not found." });
            }

            _appDbContext.StudentRegistrations.Remove(registration);
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
