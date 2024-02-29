
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

namespace Bono.Employees.Application.Services
{
    public class UserRoleService : IUserRoleService
    {

        private readonly IUserRoleRepository UserRoleRepository;
        private readonly IMapper mapper;
        private readonly ValidationResult validationResult;

        public UserRoleService(IUserRoleRepository UserRoleRepository, IMapper mapper, ValidationResult validationResult)
        {
            this.UserRoleRepository = UserRoleRepository;
            this.mapper = mapper;
            this.validationResult = validationResult;
        }        

        public IEnumerable<UserRoleViewModel> GetAll()
        {
            var userRoles = this.UserRoleRepository.GetAll();

            List<UserRoleViewModel> _UserRoleViewModels = mapper.Map<List<UserRoleViewModel>>(userRoles);

            return _UserRoleViewModels;
        }

        public ValidationResult Post(UserRoleViewModel UserRoleViewModel)
        {
            if (UserRoleViewModel.Id != Guid.Empty)
                validationResult.Add("User Role ID must be empty");

            Validator.ValidateObject(UserRoleViewModel, new ValidationContext(UserRoleViewModel), true);

            UserRole userRole = mapper.Map<UserRole>(UserRoleViewModel);
            
            validationResult.Data = this.UserRoleRepository.Create(userRole);

            if (validationResult.Data == null)
            {
                validationResult.Add("The Entity you are trying to record is null, please try again!");
            }
            
            return validationResult;
            
        }

        public UserRoleViewModel GetById(string id)
        {
            if (!Guid.TryParse(id, out Guid UserRoleId))
                throw new Exception("UserRoleID is not valid");

            UserRole _UserRole = this.UserRoleRepository.Find(x => x.Id == UserRoleId && !x.IsDeleted);
            if (_UserRole == null)
                throw new Exception("UserRole not found");

            return mapper.Map<UserRoleViewModel>(_UserRole);
        }

        public ValidationResult Put(UserRoleViewModel UserRoleViewModel)
        {
            if (UserRoleViewModel.Id == Guid.Empty)                
                validationResult.Add("ID is invalid");

            UserRole UserRole = this.UserRoleRepository.Find(x => x.Id == UserRoleViewModel.Id && !x.IsDeleted);
            if (UserRole == null)
                validationResult.Add("UserRole not found");
            
            UserRole = mapper.Map<UserRole>(UserRoleViewModel);           
            
            try
            {
                this.UserRoleRepository.Update(UserRole, x => x.Id == UserRole.Id);
            }
            catch (Exception ex)
            {
                validationResult.Add(ex.Message);
            }

            return validationResult;
        }

        public ValidationResult Delete(string id)
        {
            if (!Guid.TryParse(id, out Guid UserRoleId))
                validationResult.Add("UserRoleID is not valid");

            UserRole _UserRole = this.UserRoleRepository.Find(x => x.Id == UserRoleId && !x.IsDeleted);
            
            if (_UserRole == null)
                throw new Exception("UserRole not found");

            try
            {
                this.UserRoleRepository.Delete(_UserRole);
            }
            catch (Exception ex)
            {
                validationResult.Add(ex.Message);
            }
           
            return validationResult;

            
        }
    }
}
