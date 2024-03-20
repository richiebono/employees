using Bono.Employees.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bono.Employees.Domain.Entities
{
    public class Company : Entity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string GovernmentId { get; set; }
        public User User { get; set; }

        public Company()
        {
        }

        public Company(string name, string address, string country, string city, string postalCode, string phoneNumber, string email, string governmentId)
        {
            Id = Guid.NewGuid();
            Name = name;
            Address = address;
            Country = country;
            City = city;
            PostalCode = postalCode;
            PhoneNumber = phoneNumber;
            Email = email;
            GovernmentId = governmentId;
            DateCreated = DateTime.Now;
            DateUpdated = DateTime.Now;
            IsDeleted = false;
        }
    }
}
