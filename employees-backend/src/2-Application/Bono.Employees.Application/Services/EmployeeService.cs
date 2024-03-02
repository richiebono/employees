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

            if (string.IsNullOrEmpty(employeeViewModel.EmployeeTypeId) || string.IsNullOrEmpty(employeeViewModel.UserId) || string.IsNullOrEmpty(employeeViewModel.FirstName))
            {
                _validationResult.Add("The sent object was empty.");
                return _validationResult;
            }

            Validator.ValidateObject(employeeViewModel, new ValidationContext(employeeViewModel), true);

            EmployeeType employeeType = _employeeTypeRepository.Find(new Guid(employeeViewModel.EmployeeTypeId));
            User user = _userRepository.Find(new Guid(employeeViewModel.UserId));
            Employee employee = new(employeeType, employeeViewModel.FirstName, employeeViewModel.LastName, employeeViewModel.Email, employeeViewModel.JobTitle, employeeViewModel.DateOfJoining, user);

            _validationResult.Data = _employeeRepository.Create(employee);

            return _validationResult;

        }

        public ValidationResult Put(EmployeeViewModel EmployeeViewModel)
        {
            if (EmployeeViewModel.Id == Guid.Empty)
                _validationResult.Add("EmployeeID is not valid");

            Employee employee = _employeeRepository.Find(x => x.Id == EmployeeViewModel.Id && !x.IsDeleted);
            if (employee == null)
                _validationResult.Add("Employee not found");

            if (!_validationResult.Errors.Any())
            {
                employee.FirstName = EmployeeViewModel.FirstName;
                employee.LastName = EmployeeViewModel.LastName;
                employee.Email = EmployeeViewModel.Email;
                employee.JobTitle = EmployeeViewModel.JobTitle;
                employee.DateOfJoining = EmployeeViewModel.DateOfJoining;
                employee.Type = _employeeTypeRepository.Find(new Guid(EmployeeViewModel.EmployeeTypeId));
                employee.DateUpdated = DateTime.Now;

                try
                {
                    bool updated = _employeeRepository.Update(employee, x => x.Id == employee.Id);

                    if (updated)
                        _validationResult.Data = employee;
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

            if (employee == null) return null;

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
                EmployeeTypeName = employee.Type?.Type,
                UserName = employee.User?.UserName,
                EmployeeTypeId = employee.Type?.Id == null ? employee.Type?.Id.ToString() : null,
                UserId = employee.User?.Id == null ? employee.User?.Id.ToString() : null
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
                    x.FirstName.Contains(filter.search) ||
                    x.FirstName == filter.search
                ) &&
                (
                    string.IsNullOrEmpty(filter.search) ||
                    x.LastName.Contains(filter.search) ||
                    x.LastName == filter.search
                ) &&
                (
                    string.IsNullOrEmpty(filter.search) ||
                    x.Email.Contains(filter.search) ||
                    x.Email == filter.search
                ) &&
                (
                    string.IsNullOrEmpty(filter.search) ||
                    x.JobTitle.Contains(filter.search) ||
                    x.JobTitle == filter.search
                ) &&
                (
                    string.IsNullOrEmpty(filter.search) ||
                    x.DateOfJoining.Contains(filter.search) ||
                    x.DateOfJoining == filter.search
                ) &&
                // (
                //     string.IsNullOrEmpty(filter.type) ||
                //     x.Type.Id == new Guid(filter.type)
                // ) &&
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
                UserName = x.User?.UserName,
                EmployeeTypeId = x.Type?.Id == null ? x.Type?.Id.ToString() : null,
                UserId = x.User?.Id == null ? x.User?.Id.ToString() : null
            }).ToList();

            if (!string.IsNullOrEmpty(filter.order) && filter.order.ToLower() == "desc")
            {
                if (filter.sort == "id")
                    employeeViewModels = employeeViewModels.OrderByDescending(x => x.Id).ToList();

                if (filter.sort == "firstName")
                    employeeViewModels = employeeViewModels.OrderByDescending(x => x.FirstName).ToList();

                if (filter.sort == "lastName")
                    employeeViewModels = employeeViewModels.OrderByDescending(x => x.LastName).ToList();

                if (filter.sort == "email")
                    employeeViewModels = employeeViewModels.OrderByDescending(x => x.Email).ToList();

                if (filter.sort == "jobTitle")
                    employeeViewModels = employeeViewModels.OrderByDescending(x => x.JobTitle).ToList();

                if (filter.sort == "dateOfJoining")
                    employeeViewModels = employeeViewModels.OrderByDescending(x => x.DateOfJoining).ToList();

                if (filter.sort == "dataCreated")
                    employeeViewModels = employeeViewModels.OrderByDescending(x => x.DateCreated).ToList();

                if (filter.sort == "dateUpdated")
                    employeeViewModels = employeeViewModels.OrderByDescending(x => x.DateUpdated).ToList();

                if (filter.sort == "EmployeeTypeName")
                    employeeViewModels = employeeViewModels.OrderByDescending(x => x.EmployeeTypeName).ToList();
            }
            else
            {
                if (filter.sort == "id")
                    employeeViewModels = employeeViewModels.OrderBy(x => x.Id).ToList();

                if (filter.sort == "firstName")
                    employeeViewModels = employeeViewModels.OrderBy(x => x.FirstName).ToList();

                if (filter.sort == "lastName")
                    employeeViewModels = employeeViewModels.OrderBy(x => x.LastName).ToList();

                if (filter.sort == "email")
                    employeeViewModels = employeeViewModels.OrderBy(x => x.Email).ToList();

                if (filter.sort == "jobTitle")
                    employeeViewModels = employeeViewModels.OrderBy(x => x.JobTitle).ToList();

                if (filter.sort == "dateOfJoining")
                    employeeViewModels = employeeViewModels.OrderBy(x => x.DateOfJoining).ToList();

                if (filter.sort == "dateCreated")
                    employeeViewModels = employeeViewModels.OrderBy(x => x.DateCreated).ToList();

                if (filter.sort == "dateUpdated")
                    employeeViewModels = employeeViewModels.OrderBy(x => x.DateUpdated).ToList();

                if (filter.sort == "EmployeeTypeName")
                    employeeViewModels = employeeViewModels.OrderBy(x => x.EmployeeTypeName).ToList();
            }

            return employeeViewModels.Skip(filter.start).Take(filter.size + 1).ToList();
        }

        public int Count(FilterViewModel filter)
        {
            return _employeeRepository.Query(x => (filter.search == null || x.FirstName.Contains(filter.search)) && !x.IsDeleted).OrderBy(x => x.FirstName).Count();
        }
    }
}
