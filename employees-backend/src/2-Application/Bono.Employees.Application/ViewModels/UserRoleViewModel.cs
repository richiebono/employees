using System;
using System.Collections.Generic;
using System.Text;
using Bono.Employees.Domain.Models;

namespace Bono.Employees.Application.ViewModels
{
    public class UserRoleViewModel : EntityViewModel
    {
        public UserViewModel User { get; private set; }
        public RoleViewModel Role { get; private set; }
    }
}
