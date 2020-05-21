using System;
using System.Collections.Generic;
using System.Text;
using RentalMovies.Domain.Enums;

namespace RentalMovies.Application.Common.Models.Requests
{
    public class UserRegistrationRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole? Role { get; set; }
    }
}
