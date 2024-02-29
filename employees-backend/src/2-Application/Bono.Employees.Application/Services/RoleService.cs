
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
    public class RoleService : IRoleService
    {

        private readonly IRoleRepository RoleRepository;
        private readonly IMapper mapper;
        private readonly ValidationResult validationResult;

        public RoleService(IRoleRepository RoleRepository, IMapper mapper, ValidationResult validationResult)
        {
            this.RoleRepository = RoleRepository;
            this.mapper = mapper;
            this.validationResult = validationResult;
        }        

        public IEnumerable<RoleViewModel> GetAll()
        {
            var Roles = this.RoleRepository.GetAll();

            List<RoleViewModel> _RoleViewModels = mapper.Map<List<RoleViewModel>>(Roles);

            return _RoleViewModels;
        }

        public ValidationResult Post(RoleViewModel RoleViewModel)
        {
            if (RoleViewModel.Id != Guid.Empty)
                validationResult.Add("Role ID must be empty");

            Validator.ValidateObject(RoleViewModel, new ValidationContext(RoleViewModel), true);

            Role Role = mapper.Map<Role>(RoleViewModel);
            
            validationResult.Data = this.RoleRepository.Create(Role);

            if (validationResult.Data == null)
            {
                validationResult.Add("The Entity you are trying to record is null, please try again!");
            }
            
            return validationResult;
            
        }

        public RoleViewModel GetById(string id)
        {
            if (!Guid.TryParse(id, out Guid RoleId))
                throw new Exception("Role is not valid");

            Role _Role = this.RoleRepository.Find(x => x.Id == RoleId && !x.IsDeleted);
            if (_Role == null)
                throw new Exception("Role not found");

            return mapper.Map<RoleViewModel>(_Role);
        }

        public ValidationResult Put(RoleViewModel RoleViewModel)
        {
            if (RoleViewModel.Id == Guid.Empty)                
                validationResult.Add("ID is invalid");

            Role Role = this.RoleRepository.Find(x => x.Id == RoleViewModel.Id && !x.IsDeleted);
            if (Role == null)
                validationResult.Add("Role not found");
            
            Role = mapper.Map<Role>(RoleViewModel);           
            
            try
            {
                this.RoleRepository.Update(Role, x => x.Id == Role.Id);
            }
            catch (Exception ex)
            {
                validationResult.Add(ex.Message);
            }

            return validationResult;
        }

        public ValidationResult Delete(string id)
        {
            if (!Guid.TryParse(id, out Guid RoleId))
                validationResult.Add("ID is not valid");

            Role _Role = this.RoleRepository.Find(x => x.Id == RoleId && !x.IsDeleted);
            
            if (_Role == null)
                throw new Exception("Role not found");

            try
            {
                this.RoleRepository.Delete(_Role);
            }
            catch (Exception ex)
            {
                validationResult.Add(ex.Message);
            }
           
            return validationResult;

            
        }
    }
}
