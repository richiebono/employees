using Bono.Employees.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bono.Employees.Domain.Interfaces.Repository
{
    public interface ICompanyRepository : IRepository<Company>
    {
        IEnumerable<Company> GetAll();
        Task<Company> GetById(Guid id);      
    }
}
