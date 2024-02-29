using System;
using System.Collections.Generic;
using System.Text;
using Bono.Employees.Application.ViewModels;

namespace Bono.Employees.Application.Interfaces
{
    public interface IEmployeeTypeService
    {
        int Count(FilterViewModel filter);
        IEnumerable<EmployeeTypeViewModel> Filter(FilterViewModel filter);
        EmployeeTypeViewModel GetById(string id);
        
    }
}
