using Microsoft.Extensions.DependencyInjection;
using System;
using Bono.Employees.Application.Interfaces;
using Bono.Employees.Application.Services;
using Bono.Employees.Data.Repositories;
using Bono.Employees.Domain.Interfaces.Repository;
using Bono.Employees.Domain.Validations;
using Bono.Employees.Infrastructure.Utils;

namespace Bono.Employees.Infrastructure.IoC
{
    public static class NativeInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            #region Services

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IEmployeeTypeService, EmployeeTypeService>();

            #endregion

            #region Domain

            services.AddScoped<ValidationResult, ValidationResult>();

            #endregion 

            #region Repositories

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeTypeRepository, EmployeeTypeRepository>();

            #endregion

            #region Utils
                        
            services.AddScoped<Settings, Settings>();
            services.AddScoped<Security, Security>();
            

            #endregion 
        }
    }
}
