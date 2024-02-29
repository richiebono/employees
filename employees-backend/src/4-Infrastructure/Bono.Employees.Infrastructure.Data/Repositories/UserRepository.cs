using System.Collections.Generic;
using Bono.Employees.Data.Context;
using Bono.Employees.Domain.Entities;
using Bono.Employees.Domain.Interfaces.Repository;

namespace Bono.Employees.Data.Repositories
{
    public class UserRepository: Repository<User>, IUserRepository
    {

        public UserRepository(BonoEmployeeContext context)
            : base(context) { }

        public IEnumerable<User> GetAll()
        {
            return Query(x => !x.IsDeleted);
        }

    }
}
