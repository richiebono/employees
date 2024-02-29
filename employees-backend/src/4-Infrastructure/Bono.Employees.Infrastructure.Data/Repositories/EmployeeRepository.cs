using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bono.Employees.Data.Context;
using Bono.Employees.Domain.Entities;
using Bono.Employees.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Bono.Employees.Data.Repositories
{
    public class EmployeeRepository: Repository<Employee>, IEmployeeRepository
    {

        public EmployeeRepository(BonoEmployeeContext context) : base(context) 
        {
            context.Employee.Include(x => x.Type).Include(x=> x.User);
        }

        public IEnumerable<Employee> GetAll()
        {
            return Query(x => !x.IsDeleted);
        }

        public new IQueryable<Employee> Query(Expression<Func<Employee, bool>> where)
        {
            return _context.Employee.Include(x => x.User).Include(x => x.Type).Where(where);
        }

        public new Employee Find(Expression<Func<Employee, bool>> where)
        {
            return _context.Employee.Include(x => x.User).Include(x => x.Type).Where(where).FirstOrDefault();
        }
    }
}
