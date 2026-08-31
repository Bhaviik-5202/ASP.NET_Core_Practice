using EmployeeLeaveManagementAPI.Data;
using EmployeeLeaveManagementAPI.DTOs;
using EmployeeLeaveManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLeaveManagementAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class LeaveController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LeaveController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("leave")]
        public async Task<IActionResult> ApplyLeave(LeaveApplicationDto leaveDto)
        {
            // Convert DTO to Model
            var leave = new LeaveApplication
            {
                EmployeeId = leaveDto.EmployeeId,
                EmployeeName = leaveDto.EmployeeName,
                LeaveType = leaveDto.LeaveType,
                FromDate = leaveDto.FromDate,
                ToDate = leaveDto.ToDate,
                Reason = leaveDto.Reason
            };

            _context.LeaveApplications.Add(leave);

            await _context.SaveChangesAsync();

            return Ok(leave);
        }

        [HttpGet("leave/employee/{employeeId}")]
        public async Task<IActionResult> GetLeavesByEmployee(int employeeId)
        {
            var leaves = await _context.LeaveApplications
                .Where(x => x.EmployeeId == employeeId)
                .ToListAsync();

            return Ok(leaves);
        }

        [HttpGet("Leaves")]
        public async Task<IActionResult> GetAllLeaves()
        {
            var leaves = await _context.LeaveApplications
                .ToListAsync();

            return Ok(leaves);
        }

        [HttpGet("Leaves/{id}")]
        public async Task<IActionResult> GetLeaveById(int id)
        {
            var leave = await _context.LeaveApplications
                .FindAsync(id);

            if (leave == null)
            {
                return NotFound(new
                {
                    message = "Leave application not found."
                });
            }

            return Ok(leave);
        }

        [HttpPut("LeaveUpdate/{id}")]
        public async Task<IActionResult> UpdateLeave(
            int id,
            LeaveApplicationDto leaveDto)
        {
            var existing = await _context.LeaveApplications
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.LeaveId == id);

            if (existing == null)
            {
                return NotFound(new
                {
                    message = "Leave application not found."
                });
            }

            // Convert DTO to Model
            var leave = new LeaveApplication
            {
                LeaveId = id,
                EmployeeId = leaveDto.EmployeeId,
                EmployeeName = leaveDto.EmployeeName,
                LeaveType = leaveDto.LeaveType,
                FromDate = leaveDto.FromDate,
                ToDate = leaveDto.ToDate,
                Reason = leaveDto.Reason
            };

            _context.LeaveApplications.Update(leave);

            await _context.SaveChangesAsync();

            return Ok(leave);
        }

        [HttpDelete("LeaveDelete/{id}")]
        public async Task<IActionResult> DeleteLeave(int id)
        {
            var leave = await _context.LeaveApplications.FindAsync(id);

            if (leave == null)
            {
                return NotFound(new
                {
                    message = "Leave application not found."
                });
            }

            _context.LeaveApplications.Remove(leave);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Leave application deleted successfully."
            });
        }
    }
}
