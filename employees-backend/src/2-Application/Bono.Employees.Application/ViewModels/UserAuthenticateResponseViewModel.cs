using System;
using System.Collections.Generic;
using System.Text;
using Bono.Employees.Domain.Entities;

namespace Bono.Employees.Application.ViewModels
{
    public class UserAuthenticateResponseViewModel
    {
        public UserAuthenticateResponseViewModel(UserViewModel user, string token)
        {
            this.User = user;
            this.Token = token;
        }

        public UserViewModel User { get; set; }
        public string Token { get; set; }
    }
}
