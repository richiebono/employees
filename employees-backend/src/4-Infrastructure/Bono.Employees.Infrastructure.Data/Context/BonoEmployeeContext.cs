using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Bono.Employees.Data.Extensions;
using Bono.Employees.Data.Mappings;
using Bono.Employees.Domain.Entities;
using Bono.Employees.Infrastructure.Utils;

namespace Bono.Employees.Data.Context
{
    public class BonoEmployeeContext: DbContext
    {
        private readonly Settings settings;
        private readonly Security security;

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeType> EmployeeType { get; set; }

        public BonoEmployeeContext(DbContextOptions<BonoEmployeeContext> option, Settings settings, Security security) : base(option) 
        {
            this.settings = settings;
            this.security = security;

            // uncomment this line before deploying to production
            // if (settings.IsDevelopment)
            // {
                Database.Migrate();
            // }
            

        }

        #region "DBSets"

        

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new EmployeeMap());
            modelBuilder.ApplyGlobalConfigurations();
            modelBuilder.SeedData(security);
            base.OnModelCreating(modelBuilder);
        }
    }
}
