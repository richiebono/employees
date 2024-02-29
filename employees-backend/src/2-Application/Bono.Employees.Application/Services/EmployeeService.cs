using AutoMapper;
using System;
using System.Collections.Generic;
using Bono.Employees.Application.Interfaces;
using Bono.Employees.Application.ViewModels;
using Bono.Employees.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;
using ValidationResult = Bono.Employees.Domain.Validations.ValidationResult;
using Bono.Employees.Domain.Interfaces.Repository;
using Bono.Employees.Domain.Interfaces.Business;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Bono.Employees.Application.Services
{
    public class EmployeeService : IEmployeeService
    {

        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmployeeTypeRepository _employeeTypeRepository;
        private readonly IMapper _mapper;
        private readonly ValidationResult _validationResult;

        public EmployeeService(IEmployeeRepository EmployeeRepository,
            IUserRepository userRepository,
            IEmployeeTypeRepository EmployeeTypeRepository,
            IMapper mapper,
            ValidationResult validationResult)
        {
            _employeeRepository = EmployeeRepository;
            _userRepository = userRepository;
            _employeeTypeRepository = EmployeeTypeRepository;
            _mapper = mapper;
            _validationResult = validationResult;
        }

        public ValidationResult Post(EmployeeViewModel employeeViewModel)
        {
            if (employeeViewModel.Id != Guid.Empty)
                _validationResult.Add("EmployeeID must be empty");

            if (string.IsNullOrEmpty(employeeViewModel.EmployeeTypeId) || string.IsNullOrEmpty(employeeViewModel.UserId) || string.IsNullOrEmpty(EmployeeViewModel.firstName))
            {
                _validationResult.Add("The sent object was empty.");
                return _validationResult;
            }

            Validator.ValidateObject(employeeViewModel, new ValidationContext(employeeViewModel), true);

            EmployeeType employeeType = _employeeTypeRepository.Find(new Guid(employeeViewModel.EmployeeTypeId));
            User user = _userRepository.Find(new Guid(employeeViewModel.UserId));
            Employee employee = new(employeeType, employeeViewModel.firstName, user);

            _validationResult.Data = _employeeRepository.Create(employee);

            return _validationResult;

        }

        public ValidationResult Put(EmployeeViewModel EmployeeViewModel)
        {
            if (EmployeeViewModel.Id == Guid.Empty)
                _validationResult.Add("EmployeeID is not valid");

            Employee Employee = _employeeRepository.Find(x => x.Id == EmployeeViewModel.Id && !x.IsDeleted);
            if (Employee == null)
                _validationResult.Add("Employee not found");

            if (!_validationResult.Errors.Any())
            {
                Employee.firstName = EmployeeViewModel.firstName;
                Employee.Type = _employeeTypeRepository.Find(new Guid(EmployeeViewModel.EmployeeTypeId));
                Employee.DateUpdated = DateTime.Now;

                try
                {
                    bool updated = _employeeRepository.Update(Employee, x => x.Id == Employee.Id);

                    if (updated)
                        _validationResult.Data = Employee;
                    else
                        _validationResult.Add("Employee not updated");
                }
                catch (Exception ex)
                {
                    _validationResult.Add(ex.Message);
                }
            }

            return _validationResult;
        }

        public ValidationResult Delete(string id)
        {
            if (!Guid.TryParse(id, out Guid EmployeeId))
                _validationResult.Add("EmployeeID is not valid");

            Employee _employee = _employeeRepository.Find(x => x.Id == EmployeeId && !x.IsDeleted);

            if (_employee == null)
                _validationResult.Add("Employee not found");

            try
            {
                _employeeRepository.Delete(_employee);
            }
            catch (Exception ex)
            {
                _validationResult.Add(ex.Message);
            }

            return _validationResult;


        }

        public IEnumerable<EmployeeViewModel> GetAll()
        {
            var Employees = _employeeRepository.GetAll();

            List<EmployeeViewModel> _employeeViewModels = _mapper.Map<List<EmployeeViewModel>>(Employees);

            return _employeeViewModels;
        }

        public EmployeeViewModel GetById(string id)
        {
            if (!Guid.TryParse(id, out Guid EmployeeId))
                throw new Exception("EmployeeID is not valid");

            Employee employee = _employeeRepository.Find(x => x.Id == EmployeeId && !x.IsDeleted);

            return new EmployeeViewModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                JobTitle = employee.JobTitle,
                DateOfJoining = employee.DateOfJoining,
                DateCreated = employee.DateCreated,
                DateUpdated = employee.DateUpdated,
                EmployeeTypeName = employee.Type.Type,
                UserName = employee.User.UserName,
                EmployeeTypeId = employee.Type.Id.ToString(),
                UserId = employee.User.Id.ToString()
            };
        }

        public IEnumerable<EmployeeViewModel> GetAll(string userId)
        {
            if (!Guid.TryParse(userId, out Guid UserId))
                throw new Exception("User is not valid");

            var employees = _employeeRepository.Query(x => x.User.Id == UserId && !x.IsDeleted);

            List<EmployeeViewModel> _employeeViewModels = _mapper.Map<List<EmployeeViewModel>>(employees);

            return _employeeViewModels;
        }

        public IEnumerable<EmployeeViewModel> Filter(FilterViewModel filter)
        {
            var employees = _employeeRepository.Query(x => 
                (
                    string.IsNullOrEmpty(filter.search) || 
                    x.firstName.Contains(filter.search) || 
                    x.firstName == filter.search
                ) && 
                (
                    string.IsNullOrEmpty(filter.type) || 
                    x.Type.Id == new Guid(filter.type)
                ) &&
                !x.IsDeleted
            ).ToList();

            List<EmployeeViewModel> employeeViewModels = employees.Select(x => new EmployeeViewModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                JobTitle = x.JobTitle,
                DateOfJoining = x.DateOfJoining,
                DateCreated = x.DateCreated,
                DateUpdated = x.DateUpdated,
                EmployeeTypeName = x.Type.Type,
                UserName = x.User.UserName,
                EmployeeTypeId = x.Type.Id.ToString(),
                UserId = x.User.Id.ToString()
            }).ToList();

            if (!string.IsNullOrEmpty(filter.Employee) && filter.Employee.ToLower() == "desc")
            {
                if (filter.sort == "id")
                    employeeViewModels = employeeViewModels.EmployeeByDescending(x => x.Id).ToList();

                if (filter.sort == "firstName")
                    employeeViewModels = employeeViewModels.EmployeeByDescending(x => x.firstName).ToList();

                if (filter.sort == "lastName")
                    employeeViewModels = employeeViewModels.EmployeeByDescending(x => x.lastName).ToList();

                if (filter.sort == "email")
                    employeeViewModels = employeeViewModels.EmployeeByDescending(x => x.email).ToList();

                if (filter.sort == "jobTitle")
                    employeeViewModels = employeeViewModels.EmployeeByDescending(x => x.jobTitle).ToList();

                if (filter.sort == "dateOfJoining")
                    employeeViewModels = employeeViewModels.EmployeeByDescending(x => x.dateOfJoining).ToList();

                if (filter.sort == "dataCreated")
                    employeeViewModels = employeeViewModels.EmployeeByDescending(x => x.DateCreated).ToList();

                if (filter.sort == "dateUpdated")
                    EmployeeViewModels = EmployeeViewModels.EmployeeByDescending(x => x.DateUpdated).ToList();

                if (filter.sort == "EmployeeTypeName")
                    employeeViewModels = employeeViewModels.EmployeeByDescending(x => x.EmployeeTypeName).ToList();
            }
            else
            {
                if (filter.sort == "id")
                    employeeViewModels = employeeViewModels.EmployeeBy(x => x.Id).ToList();

                if (filter.sort == "firstName")
                    employeeViewModels = employeeViewModels.EmployeeBy(x => x.firstName).ToList();

                if (filter.sort == "lastName")
                    employeeViewModels = employeeViewModels.EmployeeBy(x => x.lastName).ToList();

                if (filter.sort == "email")
                    employeeViewModels = employeeViewModels.EmployeeBy(x => x.email).ToList();

                if (filter.sort == "jobTitle")
                    employeeViewModels = employeeViewModels.EmployeeBy(x => x.jobTitle).ToList();

                if (filter.sort == "dateOfJoining")
                    employeeViewModels = employeeViewModels.EmployeeBy(x => x.dateOfJoining).ToList();

                if (filter.sort == "dateCreated")
                    employeeViewModels = employeeViewModels.EmployeeBy(x => x.DateCreated).ToList();

                if (filter.sort == "dateUpdated")
                    employeeViewModels = employeeViewModels.EmployeeBy(x => x.DateUpdated).ToList();

                if (filter.sort == "EmployeeTypeName")
                    employeeViewModels = employeeViewModels.EmployeeBy(x => x.EmployeeTypeName).ToList();
            }

            return EmployeeViewModels.Skip(filter.start).Take(filter.size+1).ToList();
        }

        public int Count(FilterViewModel filter)
        {
            return _employeeRepository.Query(x => (filter.search == null || x.firstName.Contains(filter.search)) && !x.IsDeleted).EmployeeBy(x => x.firstName).Count();
        }
    }
}
