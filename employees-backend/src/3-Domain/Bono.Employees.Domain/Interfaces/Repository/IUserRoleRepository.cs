using System.Collections.Generic;
using Bono.Employees.Domain.Entities;

namespace Bono.Employees.Domain.Interfaces.Repository
{
    public interface IUserRoleRepository: IRepository<UserRole>
    {
        IEnumerable<UserRole> GetAll();
    }
}
