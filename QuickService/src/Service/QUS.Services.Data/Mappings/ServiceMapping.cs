using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QUS.Services.Domain.Enums;
using QUS.Services.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Services.Data.Mappings
{
    public class ServiceMapping : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProviderId)
                .IsRequired();

            builder.Property(x => x.Title)
                .IsRequired();
                
            builder.Property(x => x.Description)
                .IsRequired();

            builder.Property(x => x.Price)
                .IsRequired();

            builder.Property(x => x.Category)
                .IsRequired()
                .HasConversion(
                   categoryConversion => categoryConversion.ToString(),
                   category => (Category)Enum.Parse(typeof(Category), category));
        }
    }
}
