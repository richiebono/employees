using System;
using System.Collections.Generic;
using Bono.Employees.Data.Context;
using Bono.Employees.Domain.Entities;
using Bono.Employees.Domain.Interfaces.Repository;

namespace Bono.Employees.Data.Repositories
{
    public class RoleRepository: Repository<Role>, IRoleRepository
    {

        public RoleRepository(BonoEmployeeContext context)
            : base(context) { }

        public IEnumerable<Role> GetAll()
        {
            return Query(x => !x.IsDeleted);
        }

    }
}
