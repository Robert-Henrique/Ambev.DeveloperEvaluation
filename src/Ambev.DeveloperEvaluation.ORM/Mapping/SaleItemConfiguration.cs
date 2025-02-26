using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.ToTable("SaleItems");
        builder.HasKey(si => si.Id);
        builder.Property(si => si.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
        builder.Property(si => si.Quantity).IsRequired();

        builder.OwnsOne(si => si.Product, product =>
        {
            product.Property(p => p.Id)
                .HasColumnName("ProductId")
                .IsRequired();

            product.Property(p => p.Name)
                .HasColumnName("ProductName")
                .IsRequired()
                .HasMaxLength(200);
        });

        builder.OwnsOne(si => si.UnitPrice, price =>
        {
            price.Property(p => p.Value)
                .HasColumnName("UnitPrice")
                .IsRequired()
                .HasColumnType("decimal(18,2)");
        });

        builder.Property(si => si.Discount).HasColumnType("decimal(18,2)");

        builder.Ignore(si => si.TotalPrice);
    }
}