using System;
using System.Collections.Generic;
using System.Text;

namespace Bono.Employees.Domain.Interfaces.Specifications
{
    public interface ISpecification<in TEntity>
    {
        bool IsSatisfiedBy(TEntity entity);
    }
}
