using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Bono.Employees.Application.ViewModels;
using Bono.Employees.Domain.Entities;

namespace Bono.Employees.Application.AutoMapper
{
    public class AutoMapperSetup: Profile
    {
        public AutoMapperSetup()
        {
            // It's important to map the view model to the entity and vice versa,
            // because the view model is the object that will be used in the API, and the entity is the object that will be used in the database.
            // So, when the API receives a request, it will receive a view model, and it will need to convert it to an entity to save it in the database.
            // In this case we avoid to use the entity directly in the API, because it can have some sensitive data that we don't want to expose.
            CreateMap<UserViewModel, User>();
            CreateMap<User, UserViewModel>();
            CreateMap<EmployeeViewModel, Employee>();
            CreateMap<Employee, EmployeeViewModel>();
            CreateMap<EmployeeTypeViewModel, EmployeeType>();
            CreateMap<EmployeeType, EmployeeTypeViewModel>();
        }
    }
}
