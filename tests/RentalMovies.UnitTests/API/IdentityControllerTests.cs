using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using RentalMovies.Application.Common.Interfaces;
using RentalMovies.Application.Common.Models;
using RentalMovies.Application.Common.Models.Requests;
using RentalMovies.Presentation.Controllers;
using Xunit;

namespace RentalMovies.UnitTests.API
{
    public class IdentityControllerTests
    {
        public readonly IIdentityService IdentityServiceFake;
        public IdentityControllerTests()
        {
            IdentityServiceFake = A.Fake<IIdentityService>();

            ConfigureIdentityServiceFake(IdentityServiceFake);
        }
        [Fact]
        public async Task RegisterUserTest()
        {
            //Arrange
            var controller = new IdentityController(IdentityServiceFake);

            //Act
            var result = await controller.Register(new UserRegistrationRequest()
            {
                Password = "somePass0123!",
                Email = "test01@test.com"
            });


            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();

        }

        [Fact]
        public async Task LoginUserTest()
        {
            //Arrange
            var controller = new IdentityController(IdentityServiceFake);

            //Act
            var result = await controller.Login(new UserLoginRequest()
            {
                Password = "somePass0123!",
                Email = "test01@test.com"
            });

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }
        private void ConfigureIdentityServiceFake(IIdentityService identityServiceFake)
        {
            A.CallTo(() => identityServiceFake.LoginAsync(A<string>._, A<string>._)).Returns(GenerateAuthenticationResult());

            A.CallTo(() => identityServiceFake.RegisterAsync(A<string>._, A<string>._, null))
                .Returns(GenerateAuthenticationResult());

            A.CallTo(() => identityServiceFake.RefreshTokenAsync(A<string>._, A<string>._))
                .Returns(GenerateAuthenticationResult());

        }
        public Task<AuthenticationResult> GenerateAuthenticationResult()
        {
            return Task.Run(() =>
            {
                return new AuthenticationResult()
                {
                    Token = "0fe563fe-94e1-4e38-bd84-0d527b0e9e32",
                    Errors = null,
                    RefreshToken = "0fe563fe-94e1-4e38-bd84-0d527b0e9e32",
                    Success = true
                };
            });
        }
    }
}
