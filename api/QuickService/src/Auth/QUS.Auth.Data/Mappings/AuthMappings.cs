using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QUS.Auth.Domain.Models;
using QUS.Auth.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Auth.Data.Mappings
{
    public class AuthMappings : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasConversion(
                    email => email.Entrada,
                    address => new Email(address)
                );

            builder.Property(x => x.Password)
                .IsRequired();

        }
    }
}
