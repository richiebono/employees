using System;
using System.Collections.Generic;
using System.Text;
using Bono.Employees.Domain.Models;

namespace Bono.Employees.Domain.Entities
{
    public class UserRole: Entity
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
