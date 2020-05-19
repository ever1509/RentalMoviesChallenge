using System.Threading.Tasks;
using RentalMovies.Application.Common.Models;
using RentalMovies.Domain.Enums;

namespace RentalMovies.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> LoginAsync(string email, string password);
        Task<(AuthenticationResult Result, string UserId)> RegisterAsync(string userName, string password,UserRole? role);
        Task<AuthenticationResult> DeleteUserAsync(string userId);
        Task<string> GetUserNameAsync(string userId);
        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);

    }
}
