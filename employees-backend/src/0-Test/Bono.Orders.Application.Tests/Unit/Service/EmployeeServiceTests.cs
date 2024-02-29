using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using Bono.Employees.Application.AutoMapper;
using Bono.Employees.Application.Services;
using Bono.Employees.Application.ViewModels;
using Bono.Employees.Domain.Entities;
using Xunit;
using Bono.Employees.Domain.Interfaces.Repository;
using ValidationResult = Bono.Employees.Domain.Validations.ValidationResult;
using System.Linq;

namespace Bono.Employees.Application.Tests.Unit.Services
{
    public class EmployeeServiceTests
    {
        private readonly EmployeeService employeeService;

        public EmployeeServiceTests()
        {
            employeeService = new EmployeeService(new Mock<IEmployeeRepository>().Object, new Mock<IUserRepository>().Object, new Mock<IEmployeeTypeRepository>().Object, new Mock<IMapper>().Object, new Mock<ValidationResult>().Object);
        }

        #region ValidatingSendingID

        [Fact]
        public void PostSendingValidId()
        {            
            var result = employeeService.Post(new EmployeeViewModel { Id = Guid.NewGuid() });
            Assert.Contains("EmployeeID must be empty", result.Errors.Select(x => x.Message).ToList());
        }

        [Fact]
        public void GetByIdSendingEmptyGuid()
        {
            var exception = Assert.Throws<Exception>(() => employeeService.GetById(""));
            Assert.Equal("EmployeeID is not valid", exception.Message);
        }

        [Fact]
        public void PutSendingEmptyGuid()
        {
            var result = employeeService.Put(new EmployeeViewModel());
            Assert.Contains("EmployeeID is not valid", result.Errors.Select(x => x.Message).ToList());
        }

        [Fact]
        public void DeleteSendingEmptyGuid()
        {            
            var result = employeeService.Delete("");
            Assert.Contains("EmployeeID is not valid", result.Errors.Select(x => x.Message).ToList());            
        }

        #endregion

        #region ValidatingCorrectObject

        [Fact]
        public void PostSendingValidObject()
        {
            var result = employeeService.Post(new EmployeeViewModel { firstName = "Richie Bono", lastName = "de Oliveira", UserId = Guid.NewGuid().ToString(), EmployeeTypeId = Guid.NewGuid().ToString() });
            Assert.True(result.IsValid);
        }

        [Fact]
        public void GetValidatingObject()
        {
            var user = new User("Richard Bono", "admin@123", "Richard Bono", "Oliveira", "123.456.456-56", "richiebono@gmail.com", "+55 11-98547-3851");

            List<Employee> Employees = new()
            {
                new Employee(new EmployeeType("Standard"), "Client 1", user)
            };
            
            var employeeRepository = new Mock<IEmployeeRepository>();
            employeeRepository.Setup(x => x.GetAll()).Returns(Employees);
            var autoMapperProfile = new AutoMapperSetup();
            var configuration = new MapperConfiguration(x => x.AddProfile(autoMapperProfile));
            IMapper mapper = new Mapper(configuration);
            var result = employeeRepository.Object.GetAll();
            Assert.True(result.Any());
        }

        #endregion

        #region ValidatingRequiredFields

        [Fact]
        public void PostSendingInvalidObject()
        {
            EmployeeViewModel Employee = new();

            var result = employeeService.Post(Employee);
            Assert.Contains("The sent object was empty.", result.Errors.Select(x => x.Message).ToList());            
        }

        #endregion
    }
}
