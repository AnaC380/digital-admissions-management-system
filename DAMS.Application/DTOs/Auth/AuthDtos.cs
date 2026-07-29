using System.Text;

namespace DAMS.Application.DTOs.Auth
{
    public record RegisterRequest(string Name, string Email, string Password);
    public record LoginRequest(string Email, string Password);
    public record AuthResponse(string Token, string Name, string Email, string Role);
}