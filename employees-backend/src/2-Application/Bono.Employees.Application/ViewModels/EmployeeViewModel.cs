using System;
using System.ComponentModel.DataAnnotations;


namespace Bono.Employees.Application.ViewModels
{
    public class EmployeeViewModel: EntityViewModel
    {        
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string JobTitle { get; set; }

        [Required]
        public string DateOfJoining { get; set; }
        
        public string EmployeeTypeName { get; set; }

        public string EmployeeTypeId { get; set; }
        
        public string UserId { get; set; }
        public string UserName { get; internal set; }
    }
}
