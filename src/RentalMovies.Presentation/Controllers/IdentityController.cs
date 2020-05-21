using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalMovies.Application.Common.Interfaces;
using RentalMovies.Application.Common.Models;
using RentalMovies.Application.Common.Models.Requests;
using RentalMovies.Application.Common.Models.Responses;

namespace RentalMovies.Presentation.Controllers
{
    [Route("api/identity")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage))
                });
            }

            var authResponse = await _identityService.RegisterAsync(request.Email, request.Password, request.Role);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken

            });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var authResponse = await _identityService.LoginAsync(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }

        [HttpGet("logout")]
        public async Task<ActionResult<AuthSuccessResponse>> Logout()
        {
            var result = await _identityService.SignOutAsync();
            return Ok(result);
        }

        [HttpGet("forgotpass/{email}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            await _identityService.ForgotPasswordAsync(email);

            return NoContent();
        }

        [HttpGet("recoverypass/{email}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RecoveryPassword(string email)
        {
            await _identityService.RecoveryPasswordAsync(email);

            return NoContent();
        }

        [HttpGet("confirmaccount/{email}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ConfirmAccount(string email)
        {
            await _identityService.ConfirmAccountAsync(email);

            return NoContent();
        }
    }
}