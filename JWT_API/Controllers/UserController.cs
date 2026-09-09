using JWT_API.Models;
using JWT_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWT_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly TokenService _tokenService;

        public UserController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            try
            {
                const string adminEmail = "admin@gmail.com";
                const string adminPassword = "admin123";

                if (string.IsNullOrWhiteSpace(dto.email) ||
                    string.IsNullOrWhiteSpace(dto.password))
                {
                    return BadRequest(new
                    {
                        Success = false,
                        Message = "Email and password are required."
                    });
                }

                if (dto.email != adminEmail || dto.password != adminPassword)
                {
                    return Unauthorized(new
                    {
                        Success = false,
                        Message = "Invalid email or password."
                    });
                }

                var user = new User
                {
                    name = "Admin",
                    email = adminEmail,
                    password = adminPassword,
                    phone = "9999999999"
                };

                var token = _tokenService.GenerateToken(user);

                return Ok(new
                {
                    Success = true,
                    Message = "Login successful.",
                    Data = new
                    {
                        Token = token,
                        User = new
                        {
                            Name = user.name,
                            Email = user.email,
                            Role = "Admin"
                        }
                    }
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    Success = false,
                    Message = "An unexpected error occurred while processing the login."
                });
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new
            {
                Success = true,
                Message = "Protected data accessed successfully.",
                Data = new
                {
                    Id = 1,
                    Name = "Admin",
                    Email = "admin@gmail.com",
                    Role = "Admin",
                    Access = "Authenticated User"
                }
            });
        }

        [AllowAnonymous]
        [HttpGet("public")]
        public IActionResult GetPublicData()
        {
            return Ok(new
            {
                Success = true,
                Message = "Public data accessed successfully.",
                Data = new
                {
                    Application = "JWT API",
                    Access = "Public"
                }
            });
        }
    }
}
