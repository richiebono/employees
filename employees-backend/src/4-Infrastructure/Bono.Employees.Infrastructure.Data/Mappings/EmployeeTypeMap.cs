using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Bono.Employees.Domain.Entities;

namespace Bono.Employees.Data.Mappings
{
    public class EmployeeTypeMap: IEntityTypeConfiguration<EmployeeType>
    {
        public void Configure(EntityTypeBuilder<EmployeeType> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Type).IsRequired();            
        }
    }
}
