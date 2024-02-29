using System;
using System.Collections.Generic;
using Bono.Employees.Data.Context;
using Bono.Employees.Domain.Entities;
using Bono.Employees.Domain.Interfaces.Repository;

namespace Bono.Employees.Data.Repositories
{
    public class UserRoleRepository: Repository<UserRole>, IUserRoleRepository
    {

        public UserRoleRepository(BonoEmployeeContext context)
            : base(context) { }

        public IEnumerable<UserRole> GetAll()
        {
            return Query(x => !x.IsDeleted);
        }

    }
}
