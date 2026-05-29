using PruebaTecnica.Frontend.Models;

namespace PruebaTecnica.Frontend.Services
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(LoginDto loginRequest);
        Task LogoutAsync();
    }
}