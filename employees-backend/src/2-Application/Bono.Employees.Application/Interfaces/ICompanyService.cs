using Bono.Employees.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bono.Employees.Application.Interfaces
{
    public interface ICompanyService : IService<CompanyViewModel>
    {
        int Count(FilterViewModel filter);
        IEnumerable<CompanyViewModel> Filter(FilterViewModel filter);
        IEnumerable<CompanyViewModel> GetAll(string userId);
    }
}
