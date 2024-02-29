using System;
using System.Collections.Generic;
using System.Text;
using Bono.Employees.Application.ViewModels;
using Bono.Employees.Domain.Validations;

namespace Bono.Employees.Application.Interfaces
{
    public interface IEmployeeService : IService<EmployeeViewModel>
    {
        int Count(FilterViewModel filter);
        IEnumerable<EmployeeViewModel> Filter(FilterViewModel filter);
        IEnumerable<EmployeeViewModel> GetAll(string userId);        
    }
}
