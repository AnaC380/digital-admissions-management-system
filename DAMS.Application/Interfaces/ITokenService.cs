using DAMS.Domain.Entities;

namespace DAMS.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
