﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RentalMovies.Application.Common.Interfaces;
using RentalMovies.Application.Common.Models;
using RentalMovies.Domain.Enums;
using RentalMovies.Infrastructure.Mail;
using RentalMovies.Infrastructure.Mail.EmailConfiguration;

namespace RentalMovies.Infrastructure.Identity
{
    public class IdentityService:IIdentityService 
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IEmailService _emailService;
        private readonly IRentalMoviesDbContext _context;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private const string UrlResetPassword = "http://rentmovies.com/resetYourPassword/";
        private const string ConfirmationLinkAccount = "http://rentmovies.com/confirAccount/";

        public IdentityService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, JwtSettings jwtSettings, IRentalMoviesDbContext context, TokenValidationParameters tokenValidationParameters, IEmailService emailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings;
            _context = context;
            _tokenValidationParameters = tokenValidationParameters;
            _emailService = emailService;
        }

        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exist." }
                };
            }

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);

            if (!userHasValidPassword)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User/password combination is wrong" }
                };
            }

            return await GenerateAuthenticationResultForUserAsync(user);
        }

        public async Task<AuthenticationResult> RegisterAsync(string email, string password, UserRole? role)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);

            if (existingUser != null)
            {
                return (new AuthenticationResult
                {
                    Errors = new[] { "User with this email address already exists" }
                });
            }

            var newUserId = Guid.NewGuid();
            var newUser = new ApplicationUser
            {
                Id = newUserId.ToString(),
                Email = email,
                UserName = email
            };

            var createdUser = await _userManager.CreateAsync(newUser, password);

            if (!createdUser.Succeeded)
            {
                return  new AuthenticationResult
                {
                    Errors = createdUser.Errors.Select(x => x.Description)
                };
            }

            if (role.HasValue)
                await AssignRoleToUser(role.Value, newUser);


            //await _userManager.AddClaimAsync(newUser, new Claim("User.view", "true"));

            return await GenerateAuthenticationResultForUserAsync(newUser);
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var isDeleted = false;

            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user != null)
            {
                await _userManager.DeleteAsync(user);

                isDeleted = true;
            }

            return isDeleted;
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }

        public Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken)
        {
            //TODO: Pending

            throw new NotImplementedException();
        }

        public async Task<AuthenticationResult> SignOutAsync()
        {
            //TODO: Pending

            return new AuthenticationResult();

        }

        public async Task ForgotPasswordAsync(string email)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Email == email);
            if (user != null)
                _emailService.Send(SendForgotPasswordEmail(user));
        }

        public async Task RecoveryPasswordAsync(string email)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Email == email);
            if (user != null)
                _emailService.Send((SendRecoveryPasswordEmail(user)));
        }

        public async Task ConfirmAccountAsync(string email)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Email == email);
            if (user != null)
                _emailService.Send((SendConfirmationAccountEmail(user)));
        }

        private async Task<AuthenticationResult> GenerateAuthenticationResultForUserAsync(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);


            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("id", user.Id)
            };

            //In case we add policy to user by claim
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            //Adding roleClaim of User in jwt claims
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                var role = await _roleManager.FindByNameAsync(userRole);
                if (role == null) continue;
                var roleClaims = await _roleManager.GetClaimsAsync(role);
                foreach (var roleClaim in roleClaims)
                {
                    if (claims.Contains(roleClaim))
                        continue;

                    claims.Add(roleClaim);
                }
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            //var refreshToken = new RefreshToken()
            //{
            //    JwtId = token.Id,
            //    UserId = user.Id,
            //    CreationDate = DateTime.UtcNow,
            //    ExpiryDate = DateTime.UtcNow.AddMonths(6),

            //};

            //await _context.RefreshTokens.AddAsync(refreshToken);
            //await _context.SaveChangesAsync();

            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token)
                //RefreshToken = refreshToken.Token
            };
        }

        private async Task AssignRoleToUser(UserRole role, ApplicationUser user)
        {
            switch (role)
            {
                case UserRole.Admin:
                    await _userManager.AddToRoleAsync(user, "Admin");
                    break;
                case UserRole.User:
                    await _userManager.AddToRoleAsync(user, "User");
                    break;
            }
        }

        private EmailMessage SendForgotPasswordEmail(ApplicationUser user)
        {
            return new EmailMessage()
            {
                Name = user.UserName,
                Content = $"Hi {user.UserName}, Here is the link for your password reset <a href='{UrlResetPassword}'>Reset your password</a> \\n Kind regards :)",
                Subject = "Your password change request",
                FromAddresses = new List<EmailAddress>() { new EmailAddress() { Name = "Test Rent Movie", Address = "test@rentmovies.com" } },
                ToAddresses = new List<EmailAddress>() { new EmailAddress() { Name = user.UserName, Address = user.Email } }
            };
        }

        private EmailMessage SendConfirmationAccountEmail(ApplicationUser user)
        {
            return new EmailMessage()
            {
                Name = user.UserName,
                Content = $"Click the link below to confirm your user account.\\n(If you didn't request this, you can ignore this email.)\\n<a href='{ConfirmationLinkAccount}'>Confirm your account</a>",
                Subject = "Confirmation account request",
                FromAddresses = new List<EmailAddress>() { new EmailAddress() { Name = "Test Rent Movie", Address = "test@rentmovies.com" } },
                ToAddresses = new List<EmailAddress>() { new EmailAddress() { Name = user.UserName, Address = user.Email } }
            };
        }

        private EmailMessage SendRecoveryPasswordEmail(ApplicationUser user)
        {
            return new EmailMessage()
            {
                Name = user.UserName,
                Content = $"Your Movie account password has been successfully changed.\\nWe are sending this notice to ensure the privacy and security of your Movie account",
                Subject = "Recovery Movie Account",
                FromAddresses = new List<EmailAddress>() { new EmailAddress() { Name = "Test Rent Movie", Address = "test@rentmovies.com" } },
                ToAddresses = new List<EmailAddress>() { new EmailAddress() { Name = user.UserName, Address = user.Email } }
            };
        }

    }
}
