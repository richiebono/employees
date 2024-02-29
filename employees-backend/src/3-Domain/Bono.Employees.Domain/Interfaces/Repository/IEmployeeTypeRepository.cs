using Bono.Employees.Domain.Entities;
using System.Collections.Generic;

namespace Bono.Employees.Domain.Interfaces.Repository
{
    public interface IEmployeeTypeRepository : IRepository<EmployeeType>
    {
        IEnumerable<EmployeeType> GetAll();
    }
}
