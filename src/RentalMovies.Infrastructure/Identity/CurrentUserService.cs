using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using RentalMovies.Application.Common.Interfaces;

namespace RentalMovies.Infrastructure.Identity
{
    public class CurrentUserService:ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            // UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            UserId = httpContextAccessor.HttpContext?.User?.Claims.Single(x => x.Type == "id").Value;
            RoleId = httpContextAccessor.HttpContext?.User?.Claims.Single(x => x.Type == ClaimTypes.Role).Value;
        }
        public string UserId { get; }
        public string RoleId { get; set; }
    }
}
