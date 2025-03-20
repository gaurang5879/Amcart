using AuthService.Domain.Entities;

namespace AuthService.Domain.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}
