using System;
using System.Collections.Generic;
using Bono.Employees.Data.Context;
using Bono.Employees.Domain.Entities;
using Bono.Employees.Domain.Interfaces.Repository;

namespace Bono.Employees.Data.Repositories
{
    public class EmployeeTypeRepository: Repository<EmployeeType>, IEmployeeTypeRepository
    {

        public EmployeeTypeRepository(BonoEmployeeContext context)
            : base(context) { }

        public IEnumerable<EmployeeType> GetAll()
        {
            return Query(x => !x.IsDeleted);
        }

    }
}
