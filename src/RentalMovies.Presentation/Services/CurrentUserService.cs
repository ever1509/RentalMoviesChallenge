using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using RentalMovies.Application.Common.Interfaces;

namespace RentalMovies.Presentation.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public string UserId { get; }
    }
}
