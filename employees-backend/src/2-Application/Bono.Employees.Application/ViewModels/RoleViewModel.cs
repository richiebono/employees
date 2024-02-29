using System;
using System.ComponentModel.DataAnnotations;


namespace Bono.Employees.Application.ViewModels
{
    public class RoleViewModel: EntityViewModel
    {
        [Required]        
        public string Name { get; set; }        
    }
}
