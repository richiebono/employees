using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationResult = Bono.Employees.Domain.Validations.ValidationResult;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;
using Bono.Employees.Application.ViewModels;
using Bono.Employees.Domain.Interfaces.Repository;
using Bono.Employees.Application.Interfaces;
using Bono.Employees.Domain.Entities;

namespace Bono.companies.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ValidationResult _validationResult;



        public CompanyService(ICompanyRepository companyRepository, IMapper mapper, ValidationResult validationResult, IUserRepository userRepository)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _validationResult = validationResult;
            _userRepository = userRepository;
        }

        public ValidationResult Post(CompanyViewModel companyViewModel)
        {
            if (companyViewModel.Id != Guid.Empty)
                _validationResult.Add("companyID must be empty");

            if (companyViewModel == null && string.IsNullOrEmpty(companyViewModel.Name))
            {
                _validationResult.Add("The sent object was empty.");
                return _validationResult;
            }

            Validator.ValidateObject(companyViewModel, new ValidationContext(companyViewModel), true);

            User user = _userRepository.Find(new Guid(companyViewModel.UserId));
            Company company = new(companyViewModel.Name, companyViewModel.Address, companyViewModel.Country, companyViewModel.City, companyViewModel.PostalCode, companyViewModel.PhoneNumber, companyViewModel.Email, companyViewModel.GovernmentId);

            _validationResult.Data = _companyRepository.Create(company);

            return _validationResult;

        }

        public ValidationResult Put(CompanyViewModel companyViewModel)
        {
            if (companyViewModel.Id == Guid.Empty)
                _validationResult.Add("companyID is not valid");

            Company company = _companyRepository.Find(x => x.Id == companyViewModel.Id && !x.IsDeleted);
            if (company == null)
                _validationResult.Add("company not found");

            if (!_validationResult.Errors.Any())
            {
                company.Name = companyViewModel.Name;
                company.Address = companyViewModel.Address;
                company.City = companyViewModel.City;
                company.Country = companyViewModel.Country;
                company.PostalCode = companyViewModel.PostalCode;
                company.PhoneNumber = companyViewModel.PhoneNumber;
                company.Email = companyViewModel.Email;
                company.GovernmentId = companyViewModel.GovernmentId;
                company.DateUpdated = DateTime.Now;

                try
                {
                    bool updated = _companyRepository.Update(company, x => x.Id == company.Id);

                    if (updated)
                        _validationResult.Data = company;
                    else
                        _validationResult.Add("company not updated");
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
            if (!Guid.TryParse(id, out Guid companyId))
                _validationResult.Add("companyID is not valid");

            Company company = _companyRepository.Find(x => x.Id == companyId && !x.IsDeleted);

            if (company == null)
                _validationResult.Add("company not found");

            try
            {
                _companyRepository.Delete(company);
            }
            catch (Exception ex)
            {
                _validationResult.Add(ex.Message);
            }

            return _validationResult;


        }

        public IEnumerable<CompanyViewModel> GetAll()
        {
            var companies = _companyRepository.GetAll();

            List<CompanyViewModel> _companyViewModels = _mapper.Map<List<CompanyViewModel>>(companies);

            return _companyViewModels;
        }

        public CompanyViewModel GetById(string id)
        {
            if (!Guid.TryParse(id, out Guid companyId))
                throw new Exception("companyID is not valid");

            Company company = _companyRepository.Find(x => x.Id == companyId && !x.IsDeleted);

            if (company == null) return null;

            return new CompanyViewModel
            {
                Id = company.Id,
                Name = company.Name,
                Address = company.Address,
                City = company.City,
                Country = company.Country,
                PostalCode = company.PostalCode,
                PhoneNumber = company.PhoneNumber,
                Email = company.Email,
                GovernmentId = company.GovernmentId,
                UserId = company.User?.Id == null ? company.User?.Id.ToString() : null
            };
        }

        public IEnumerable<CompanyViewModel> GetAll(string userId)
        {
            if (!Guid.TryParse(userId, out Guid UserId))
                throw new Exception("User is not valid");

            var companies = _companyRepository.Query(x => x.User.Id == UserId && !x.IsDeleted);

            List<CompanyViewModel> _companyViewModels = _mapper.Map<List<CompanyViewModel>>(companies);

            return _companyViewModels;
        }

        public IEnumerable<CompanyViewModel> Filter(FilterViewModel filter)
        {
            var companies = _companyRepository.Query(x =>
                (
                    string.IsNullOrEmpty(filter.search) ||
                    x.Name.Contains(filter.search) ||
                    x.Name == filter.search
                ) &&
                (
                    string.IsNullOrEmpty(filter.search) ||
                    x.PhoneNumber.Contains(filter.search) ||
                    x.PhoneNumber == filter.search
                ) &&
                (
                    string.IsNullOrEmpty(filter.search) ||
                    x.Email.Contains(filter.search) ||
                    x.Email == filter.search
                ) &&
                !x.IsDeleted
            ).ToList();

            List<CompanyViewModel> companyViewModels = companies.Select(x => new CompanyViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                City = x.City,
                Country = x.Country,
                PostalCode = x.PostalCode,
                PhoneNumber = x.PhoneNumber,
                Email = x.Email,
                GovernmentId = x.GovernmentId,
                UserId = x.User?.Id == null ? x.User?.Id.ToString() : null
            }).ToList();

            if (!string.IsNullOrEmpty(filter.order) && filter.order.ToLower() == "desc")
            {
                if (filter.sort == "id")
                    companyViewModels = companyViewModels.OrderByDescending(x => x.Id).ToList();

                if (filter.sort == "name")
                    companyViewModels = companyViewModels.OrderByDescending(x => x.Name).ToList();


                if (filter.sort == "address")
                    companyViewModels = companyViewModels.OrderByDescending(x => x.Address).ToList();

                if (filter.sort == "city")
                    companyViewModels = companyViewModels.OrderByDescending(x => x.City).ToList();

                if (filter.sort == "country")
                    companyViewModels = companyViewModels.OrderByDescending(x => x.Country).ToList();

                if (filter.sort == "postalCode")
                    companyViewModels = companyViewModels.OrderByDescending(x => x.PostalCode).ToList();
                

                if (filter.sort == "phoneNumber")
                    companyViewModels = companyViewModels.OrderByDescending(x => x.PhoneNumber).ToList();

                if (filter.sort == "email")
                    companyViewModels = companyViewModels.OrderByDescending(x => x.Email).ToList();

                if (filter.sort == "governmentId")
                    companyViewModels = companyViewModels.OrderByDescending(x => x.GovernmentId).ToList();



            }
            else
            {
                if (filter.sort == "id")
                    companyViewModels = companyViewModels.OrderBy(x => x.Id).ToList();

                if (filter.sort == "name")
                    companyViewModels = companyViewModels.OrderBy(x => x.Name).ToList();

                if (filter.sort == "address")
                    companyViewModels = companyViewModels.OrderBy(x => x.Address).ToList();

                if (filter.sort == "city")
                    companyViewModels = companyViewModels.OrderBy(x => x.City).ToList();

                if (filter.sort == "country")
                    companyViewModels = companyViewModels.OrderBy(x => x.Country).ToList();

                if (filter.sort == "postalCode")
                    companyViewModels = companyViewModels.OrderBy(x => x.PostalCode).ToList();

                if (filter.sort == "phoneNumber")
                    companyViewModels = companyViewModels.OrderBy(x => x.PhoneNumber).ToList();

                if (filter.sort == "email")
                    companyViewModels = companyViewModels.OrderBy(x => x.Email).ToList();

                if (filter.sort == "governmentId")
                    companyViewModels = companyViewModels.OrderBy(x => x.GovernmentId).ToList();

            }

            return companyViewModels.Skip(filter.start).Take(filter.size + 1).ToList();
        }

        public int Count(FilterViewModel filter)
        {
            return _companyRepository.Query(x => (filter.search == null || x.Name.Contains(filter.search)) && !x.IsDeleted).OrderBy(x => x.Name).Count();
        }        
    }
}
