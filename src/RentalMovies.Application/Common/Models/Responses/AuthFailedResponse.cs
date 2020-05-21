using System;
using System.Collections.Generic;
using System.Text;

namespace RentalMovies.Application.Common.Models.Responses
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
