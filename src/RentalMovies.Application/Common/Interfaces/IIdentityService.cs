using System.Threading.Tasks;
using RentalMovies.Application.Common.Models;
using RentalMovies.Domain.Enums;

namespace RentalMovies.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> LoginAsync(string email, string password);
        Task<AuthenticationResult> RegisterAsync(string email, string password,UserRole? role);
        Task<bool> DeleteUserAsync(string userId);
        Task<string> GetUserNameAsync(string userId);
        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);
        Task<AuthenticationResult> SignOutAsync();
        Task ForgotPasswordAsync(string email);
        Task RecoveryPasswordAsync(string email);
        Task ConfirmAccountAsync(string email);
    }
}
