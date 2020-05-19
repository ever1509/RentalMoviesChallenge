using System.Collections.Generic;

namespace RentalMovies.Application.Common.Models
{
    public class AuthenticationResult
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
