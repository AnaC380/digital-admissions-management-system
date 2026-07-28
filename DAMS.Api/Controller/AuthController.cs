using DAMS.Application.DTOs.Auth;
using DAMS.Application.Interfaces;
using DAMS.Domain.Entities;
using DAMS.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DAMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly DamsDbContext _context;
        private readonly ITokenService _tokenService;

        // Role padrão para qualquer registro público. NUNCA aceite o role vindo do cliente.
        private const string DefaultRole = "User";

        public AuthController(DamsDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
                return Conflict(new { message = "E-mail ja cadastrado." });

            var user = new User(request.Name, request.Email, DefaultRole,
                                BCrypt.Net.BCrypt.HashPassword(request.Password));

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Register),
                new AuthResponse(_tokenService.GenerateToken(user), user.Name, user.Email, user.Role));
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return Unauthorized(new { message = "Credenciais invalidas." });

            return Ok(new AuthResponse(_tokenService.GenerateToken(user), user.Name, user.Email, user.Role));
        }

        [HttpPost("promote/{userId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PromoteToAdmin(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user is null)
                return NotFound();

            user.SetRole("Admin");
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}