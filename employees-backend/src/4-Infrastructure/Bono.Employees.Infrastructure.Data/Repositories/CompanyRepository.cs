using Bono.Employees.Data.Context;
using Bono.Employees.Data.Repositories;
using Bono.Employees.Domain.Entities;
using Bono.Employees.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bono.Employees.Infrastructure.Data.Repositories
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {

        CompanyRepository(BonoEmployeeContext context) : base(context)
        {            
        }
        
        public IEnumerable<Company> GetAll()
        {
            return Query(x => !x.IsDeleted);
        }

        public Task<Domain.Entities.Company> GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
