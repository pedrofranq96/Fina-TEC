using Fina.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fina.Api.Data.Mappings;

public class OrderMapping :IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Order");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Number).IsRequired().HasColumnType("CHAR").HasMaxLength(8);
        builder.Property(x => x.ExternalReference).IsRequired(false).HasColumnType("VARCHAR").HasMaxLength(60);
        builder.Property(x => x.Gateway).IsRequired().HasColumnType("SMALLINT");
        builder.Property(x => x.CreatedAt).IsRequired().HasColumnType("DATETIME2");
        builder.Property(x => x.UpdatedAt).IsRequired().HasColumnType("DATETIME2");
        builder.Property(x => x.Status).IsRequired().HasColumnType("SMALLINT");
        builder.Property(x => x.UserId).IsRequired(false).HasColumnType("VARCHAR").HasMaxLength(160);

        builder.HasOne(x => x.Product).WithMany();
        builder.HasOne(x => x.Voucher).WithMany();


    }
}