using System;
using System.Collections.Generic;
using System.Text;
using Bono.Employees.Domain.Models;

namespace Bono.Employees.Domain.Entities
{
    public class EmployeeType : Entity
    {
        public EmployeeType()
        {
        }

        public EmployeeType(string type)
        {
            this.Id = Guid.NewGuid();
            this.Type = type;
            this.DateCreated = DateTime.Now;
            this.DateUpdated = DateTime.Now;
            this.IsDeleted = false;            
        }
        
        public string Type { get; set; }
    }
}
