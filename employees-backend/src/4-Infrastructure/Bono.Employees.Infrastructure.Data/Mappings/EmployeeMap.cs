using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Bono.Employees.Domain.Entities;

namespace Bono.Employees.Data.Mappings
{
    public class EmployeeMap: IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.firstName).IsRequired();
            builder.Property(x => x.lastName).IsRequired();
            builder.Property(x => x.email).IsRequired();
            builder.Property(x => x.jobTitle).IsRequired();
            builder.Property(x => x.dateOfJoining).IsRequired();
        }
    }
}
