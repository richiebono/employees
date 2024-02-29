using System;
using System.Collections.Generic;
using System.Text;
using Bono.Employees.Domain.Models;

namespace Bono.Employees.Domain.Entities
{
    public class Employee : Entity
    {        
        public EmployeeType Type { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string JobTitle { get; set; }
        public string DateOfJoining { get; set; }
        public User User { get; set; }
        
        public Employee()
        {            
        }

        public Employee(EmployeeType type, string firstName, string lastName, string email, string jobTitle, string dateOfJoining, User user)
        {
            Id = Guid.NewGuid();
            Type = type;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            JobTitle = jobTitle;
            DateOfJoining = dateOfJoining;
            User = user;
            DateCreated = DateTime.Now;
            DateUpdated = DateTime.Now;
            IsDeleted = false;
        }
        
    }
}
