using System;
using System.Collections.Generic;
using System.Text;
using Bono.Employees.Domain.Models;

namespace Bono.Employees.Domain.Entities
{
    public class User: Entity
    {

        #region Parameters
        //public Guid UniqueIdentifierInCompany { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Password { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Discriminator { get; set; }
        public IEnumerable<UserRole> Roles { get; set; }

        #endregion

        #region Constructors
        protected User() { }

        public User(string UserName, string Password, string FirstName, string LastName, string Cpf, string Email, string PhoneNumber)
        {
            this.Id = Guid.NewGuid();
            this.UserName = UserName;
            this.Password = Password;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Cpf = Cpf;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
        }

        #endregion

        #region Methods

        public void SetPassword(string Password)
        {
            this.Password = Password;
        }       

        #endregion

    }
}
