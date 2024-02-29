using System;
using System.ComponentModel.DataAnnotations;

namespace Bono.Employees.Application.ViewModels
{
    public class UserViewModel: EntityViewModel
    {
        
        public string Cpf { get; set; }
        
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email")]
        
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Invalid Password", MinimumLength = 6)]
        [DataType(DataType.Password)]
        
        public string Password { get; set; }

        [DataType(DataType.Password)]
        
        [Compare("Password", ErrorMessage = "Password Confirmation Invalid")]
        public string ConfirmPassword { get; set; }

        
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]        
        public string UserName { get; set; }

        [Required]        
        public string FirstName { get; set; }

        [Required]        
        public string LastName { get; set; }


        public string Discriminator { get; set; }        
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        
        public string SecurityStamp { get; set; }
        public string[] Roles { get; set; }

    }
}
