using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bono.Employees.Application.AutoMapper;
using Bono.Employees.Application.Services;
using Bono.Employees.Application.ViewModels;
using Bono.Employees.Domain.Entities;
using Bono.Employees.Domain.Interfaces;
using Xunit;
using Bono.Employees.Domain.Interfaces.Repository;
using ValidationResult = Bono.Employees.Domain.Validations.ValidationResult;
using System.Linq;
using Bono.Employees.Infrastructure.Utils;

namespace Bono.Employees.Application.Tests.Unit.Services
{
    public class UserServiceTests
    {
        private readonly UserService userService;

        public UserServiceTests()
        {
            userService = new UserService(new Mock<IUserRepository>().Object, new Mock<IMapper>().Object, new ValidationResult(), new Security());
        }

        #region ValidatingSendingID

        [Fact]
        public void PostSendingValidId()
        {            
            var result = userService.Post(new UserViewModel { Id = Guid.NewGuid() });
            Assert.Contains("UserID must be empty", result.Errors.Select(x => x.Message).ToList());
        }

        [Fact]
        public void GetByIdSendingEmptyGuid()
        {
            var exception = Assert.Throws<Exception>(() => userService.GetById(""));
            Assert.Equal("UserID is not valid", exception.Message);
        }

        [Fact]
        public void PutSendingEmptyGuid()
        {
            var result = userService.Put(new UserViewModel());
            Assert.Contains("ID is invalid", result.Errors.Select(x => x.Message).ToList());
        }

        [Fact]
        public void DeleteSendingEmptyGuid()
        {            
            var result = userService.Delete("");
            Assert.Contains("UserID is not valid", result.Errors.Select(x => x.Message).ToList());            
        }

        [Fact]
        public void AuthenticateSendingEmptyValues()
        {
            var result = userService.Authenticate(new UserAuthenticateRequestViewModel());
            Assert.Contains("Email/Password are required.", result.Errors.Select(x=> x.Message).ToList());
        }

        #endregion

        #region ValidatingCorrectObject

        [Fact]
        public void PostSendingValidObject()
        {
            var result = userService.Post(new UserViewModel { UserName = "richiebono",  Cpf = "367.337.160-62",  FirstName = "Richard", LastName= "Bono", Email = "richiebono@gmail.com", Password = "bono@teste", ConfirmPassword = "bono@teste", PhoneNumber="(11) 98547-0000" });
            Assert.True(result.IsValid);
        }

        [Fact]
        public void GetValidatingObject()
        {
            List<User> users = new()
            {
                new User("Richard Bono", "admin@123", "Richard Bono", "Oliveira", "123.456.456-56", "richiebono@gmail.com", "+55 11-98547-3851")
            };
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.GetAll()).Returns(users);
            var autoMapperProfile = new AutoMapperSetup();
            var configuration = new MapperConfiguration(x => x.AddProfile(autoMapperProfile));
            IMapper mapper = new Mapper(configuration);            
            var result = userRepository.Object.GetAll();
            Assert.True(result.Any());
        }

        #endregion

        #region ValidatingRequiredFields

        [Fact]
        public void PostSendingInvalidObject()
        {
            UserViewModel user = new();

            var result = userService.Post(user);
            Assert.Contains("The sent object was empty.", result.Errors.Select(x => x.Message).ToList());            
        }

        #endregion
    }
}
