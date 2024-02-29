
using AutoMapper;
using System;
using System.Collections.Generic;
using Bono.Employees.Application.Interfaces;
using Bono.Employees.Application.ViewModels;
using Bono.Employees.Domain.Entities;
using ValidationResult = Bono.Employees.Domain.Validations.ValidationResult;
using Bono.Employees.Domain.Interfaces.Repository;
using System.Linq;

namespace Bono.Employees.Application.Services
{
    public class EmployeeTypeService : IEmployeeTypeService
    {

        private readonly IEmployeeTypeRepository _employeeTypeRepository;
        private readonly IMapper _mapper;

        public EmployeeTypeService(IEmployeeTypeRepository employeeTypeRepository,
            IMapper mapper)
        {
            _employeeTypeRepository = employeeTypeRepository;
            _mapper = mapper;
        }

        public IEnumerable<EmployeeTypeViewModel> GetAll()
        {
            var EmployeeTypes = _employeeTypeRepository.GetAll();

            List<EmployeeTypeViewModel> _employeeTypeViewModels = _mapper.Map<List<EmployeeTypeViewModel>>(EmployeeTypes);

            return _employeeTypeViewModels;
        }

        public EmployeeTypeViewModel GetById(string id)
        {
            if (!Guid.TryParse(id, out Guid EmployeeTypeId))
                throw new Exception("ID is not valid");

            EmployeeType _employeeType = _employeeTypeRepository.Find(x => x.Id == EmployeeTypeId && !x.IsDeleted);
            if (_employeeType == null)
                throw new Exception("EmployeeType not found");

            return _mapper.Map<EmployeeTypeViewModel>(_employeeType);
        }

        public IEnumerable<EmployeeTypeViewModel> Filter(FilterViewModel filter)
        {
            var employees = new List<EmployeeType>();

            if (!string.IsNullOrEmpty(filter.search))
            {
                employees = _employeeTypeRepository.Query(x => x.Type.Contains(filter.search) || x.Type == filter.search).ToList();
            }
            else
            {
                employees = _employeeTypeRepository.Query(x => !x.IsDeleted).ToList();
            }

            List<EmployeeTypeViewModel> employeeTypeViewModels = _mapper.Map<List<EmployeeTypeViewModel>>(employees);
            
            if (!string.IsNullOrEmpty(filter.Employee) && filter.Employee.ToLower() == "desc")
            {
                employeeTypeViewModels = employeeTypeViewModels.EmployeeByDescending(x => x.Type).ToList();
            }
            else
            {
                employeeTypeViewModels = employeeTypeViewModels.EmployeeBy(x => x.Type).ToList();
            }

            return EmployeeTypeViewModels.Skip(filter.start).Take(filter.size+1).ToList();
        }

        public int Count(FilterViewModel filter)
        {
            return _employeeTypeRepository.Query(x => (filter.search == null || x.Type.Contains(filter.search)) && !x.IsDeleted).Count();
        }
    }
}
