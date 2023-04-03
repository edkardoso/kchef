using edk.Fusc.Contracts;
using edk.Fusc.Core.Mediator;
using edk.Fusc.Core.Validators;
using edk.Kchef.Application.Features.Users.Create;
using edk.Kchef.Domain.Common.Resources;
using edk.Kchef.Domain.Contracts.Repositories;
using edk.Kchef.Domain.Contracts.Services;
using Moq;

namespace edk.Kchef.ApplicationTests
{
    public class CreateUserTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IPasswordService> _passwordService;
        private readonly UseCaseMediator _mediator;

        private readonly string _login;
        private readonly string _firstName;
        private readonly string _email;
        private readonly string _password;

        public CreateUserTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _userRepository = new Mock<IUserRepository>();
            _passwordService = new Mock<IPasswordService>();
            _mediator = new UseCaseMediator();

            _login = "userLogin";
            _firstName = "userName";
            _email = "email@provedor.com";
            _password = "@bC123456";


        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void ValidatorMustReturnErrorWhenLoginIsEmptyOrNull(string login)
        {
            // arrange
            var validator = new CreateUseCaseValidator();
            var input = new CreateUserInput(
                Login: login,
                FirstName: _firstName
                , Email: _email
                , Password: _password);

            // act
            var notifications = validator.Validate(input);

            // assert
            Assert.True(notifications.HasError());
            Assert.Equal(String.Format(UserResource.PropertyRequired, nameof(input.Login)), notifications.FirstOrDefault()?.Message);

        }
        []
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ValidatorMustReturnErrorWhenFirstNameIsEmptyOrNull(string firstName)
        {
            // arrange
            var validator = new CreateUseCaseValidator();
            var input = new CreateUserInput(
                Login: _login,
                FirstName: firstName
                , Email: _email
                , Password: _password);

            // act
            var notifications = validator.Validate(input);

            // assert
            Assert.True(notifications.HasError());
            Assert.Equal(String.Format(UserResource.PropertyRequired, nameof(input.FirstName)), notifications.FirstOrDefault()?.Message);

        }



       

    }
}