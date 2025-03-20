using AuthService.Domain.Entities;

namespace AuthService.Domain.Interfaces
{
    public interface IAuthRepository
    {
        Task<string> RegisterAsync(User user, string password);
        Task<string> LoginAsync(string email, string password);
    }
}
