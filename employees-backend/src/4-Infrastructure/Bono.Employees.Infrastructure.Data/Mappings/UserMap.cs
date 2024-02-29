using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Bono.Employees.Domain.Entities;

namespace Bono.Employees.Data.Mappings
{
    public class UserMap: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Id).IsRequired();

            builder.Property(x => x.UserName).HasMaxLength(100).IsRequired();

            builder.Property(x => x.Email).IsRequired();

            builder.Property(x => x.Password).IsRequired().HasDefaultValue("bono@teste");
        }
    }
}
