using Mc2.CrudTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Infrastructure.Persistence.EntityConfiguration
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(x => x.Firstname).HasMaxLength(100);
            builder.Property(x => x.Lastname).HasMaxLength(100);
            builder.Property(x => x.Email).HasMaxLength(200);
            builder.Property(x => x.BankAccountNumber).HasMaxLength(100);

            builder.HasIndex(p => p.Email).IsUnique();
            builder.HasIndex(p => new { p.Firstname, p.Lastname, p.DateOfBirth }).IsUnique();
        }
    }
}
