using System;
using System.Collections.Generic;
using System.Text;
using Bono.Employees.Domain.Entities;

namespace Bono.Employees.Domain.Interfaces.Repository
{
    public interface IRoleRepository: IRepository<Role>
    {
        IEnumerable<Role> GetAll();
    }
}
