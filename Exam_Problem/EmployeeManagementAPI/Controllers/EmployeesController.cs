using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.DTOs;
using EmployeeManagementAPI.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IValidator<EmployeeCreateDto> _createValidator;
        private readonly IValidator<EmployeeUpdateDto> _updateValidator;

        public EmployeesController(
            AppDbContext context,
            IValidator<EmployeeCreateDto> createValidator,
            IValidator<EmployeeUpdateDto> updateValidator)
        {
            _context = context;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        // POST: api/Employees
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeCreateDto dto)
        {
            var result = await _createValidator.ValidateAsync(dto);

            if (!result.IsValid)
                return BadRequest(new ApiResponseDto<object>
                {
                    Status = false,
                    Message = "Validation failed.",
                    Error = result.Errors
                });

            var employee = new Employee
            {
                EmployeeName = dto.EmployeeName,
                EmailAddress = dto.EmailAddress,
                MobileNumber = dto.MobileNumber,
                EmployeeCode = dto.EmployeeCode,
                Salary = dto.Salary,
                JoiningDate = dto.JoiningDate,
                BirthDate = dto.BirthDate,
                IsActive = dto.IsActive
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return StatusCode(201, new ApiResponseDto<Employee>
            {
                Status = true,
                Message = "Employee created successfully.",
                Data = employee
            });
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _context.Employees
                .AsNoTracking()
                .OrderBy(e => e.EmployeeId)
                .ToListAsync();

            return Ok(new ApiResponseDto<List<Employee>>
            {
                Status = true,
                Message = "Employees retrieved successfully.",
                Data = employees
            });
        }

        // GET: api/Employees/1
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _context.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null)
                return NotFound(new ApiResponseDto<object>
                {
                    Status = false,
                    Message = "Employee not found.",
                    Error = new { EmployeeId = id }
                });

            return Ok(new ApiResponseDto<Employee>
            {
                Status = true,
                Message = "Employee retrieved successfully.",
                Data = employee
            });
        }

        // PUT: api/Employees/1
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateEmployee(
            int id,
            EmployeeUpdateDto dto)
        {
            var result = await _updateValidator.ValidateAsync(dto);

            if (!result.IsValid)
                return BadRequest(new ApiResponseDto<object>
                {
                    Status = false,
                    Message = "Validation failed.",
                    Error = result.Errors
                });

            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
                return NotFound(new ApiResponseDto<object>
                {
                    Status = false,
                    Message = "Employee not found.",
                    Error = new { EmployeeId = id }
                });

            employee.EmployeeName = dto.EmployeeName;
            employee.EmailAddress = dto.EmailAddress;
            employee.MobileNumber = dto.MobileNumber;
            employee.EmployeeCode = dto.EmployeeCode;
            employee.Salary = dto.Salary;
            employee.JoiningDate = dto.JoiningDate;
            employee.BirthDate = dto.BirthDate;
            employee.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();

            return Ok(new ApiResponseDto<Employee>
            {
                Status = true,
                Message = "Employee updated successfully.",
                Data = employee
            });
        }

        // DELETE: api/Employees/1
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
                return NotFound(new ApiResponseDto<object>
                {
                    Status = false,
                    Message = "Employee not found.",
                    Error = new { EmployeeId = id }
                });

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok(new ApiResponseDto<object>
            {
                Status = true,
                Message = "Employee deleted successfully.",
                Data = null
            });
        }

        // GET: api/Employees/active
        [HttpGet("active")]
        public async Task<IActionResult> GetActiveEmployees()
        {
            var employees = await _context.Employees
                .AsNoTracking()
                .Where(e => e.IsActive)
                .OrderBy(e => e.EmployeeId)
                .ToListAsync();

            return Ok(new ApiResponseDto<List<Employee>>
            {
                Status = true,
                Message = "Active employees retrieved successfully.",
                Data = employees
            });
        }

        // GET: api/Employees/search?name=Rahul
        [HttpGet("search")]
        public async Task<IActionResult> SearchEmployees(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest(new ApiResponseDto<object>
                {
                    Status = false,
                    Message = "Name is required."
                });

            var employees = await _context.Employees
                .AsNoTracking()
                .Where(e => e.EmployeeName.Contains(name.Trim()))
                .OrderBy(e => e.EmployeeId)
                .ToListAsync();

            return Ok(new ApiResponseDto<List<Employee>>
            {
                Status = true,
                Message = "Employee search completed successfully.",
                Data = employees
            });
        }

        // GET: api/Employees/salary-range?min=30000&max=60000
        [HttpGet("salary-range")]
        public async Task<IActionResult> GetEmployeesBySalaryRange(
            decimal min,
            decimal max)
        {
            if (min < 0 || max < 0)
                return BadRequest(new ApiResponseDto<object>
                {
                    Status = false,
                    Message = "Salary cannot be negative."
                });

            if (min > max)
                return BadRequest(new ApiResponseDto<object>
                {
                    Status = false,
                    Message = "Minimum salary cannot be greater than maximum salary."
                });

            var employees = await _context.Employees
                .AsNoTracking()
                .Where(e => e.Salary >= min && e.Salary <= max)
                .OrderBy(e => e.Salary)
                .ToListAsync();

            return Ok(new ApiResponseDto<List<Employee>>
            {
                Status = true,
                Message = "Employees within salary range retrieved successfully.",
                Data = employees
            });
        }

        // GET: api/Employees/joined-between
        [HttpGet("joined-between")]
        public async Task<IActionResult> GetEmployeesJoinedBetween(
            DateTime from,
            DateTime to)
        {
            if (from > to)
                return BadRequest(new ApiResponseDto<object>
                {
                    Status = false,
                    Message = "From date cannot be greater than to date."
                });

            var employees = await _context.Employees
                .AsNoTracking()
                .Where(e => e.JoiningDate.Date >= from.Date &&
                            e.JoiningDate.Date <= to.Date)
                .OrderBy(e => e.JoiningDate)
                .ToListAsync();

            return Ok(new ApiResponseDto<List<Employee>>
            {
                Status = true,
                Message = "Employees joined between dates retrieved successfully.",
                Data = employees
            });
        }

        // GET: api/Employees/summary
        [HttpGet("summary")]
        public async Task<IActionResult> GetEmployeeSummary()
        {
            var total = await _context.Employees.CountAsync();
            var active = await _context.Employees.CountAsync(e => e.IsActive);

            var averageSalary = total == 0
                ? 0
                : await _context.Employees.AverageAsync(e => e.Salary);

            return Ok(new ApiResponseDto<EmployeeSummaryDto>
            {
                Status = true,
                Message = "Employee summary retrieved successfully.",
                Data = new EmployeeSummaryDto
                {
                    TotalEmployees = total,
                    ActiveEmployees = active,
                    InactiveEmployees = total - active,
                    AverageSalary = Math.Round(averageSalary, 2)
                }
            });
        }

        // GET: api/Employees/highest-paid
        [HttpGet("highest-paid")]
        public async Task<IActionResult> GetHighestPaidEmployee()
        {
            var employee = await _context.Employees
                .AsNoTracking()
                .OrderByDescending(e => e.Salary)
                .FirstOrDefaultAsync();

            if (employee == null)
                return NotFound(new ApiResponseDto<object>
                {
                    Status = false,
                    Message = "No employees found."
                });

            return Ok(new ApiResponseDto<Employee>
            {
                Status = true,
                Message = "Highest-paid employee retrieved successfully.",
                Data = employee
            });
        }
    }
}
