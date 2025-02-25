using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(s => s.Number)
            .IsRequired()
            .HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.OwnsOne(s => s.Customer, customer =>
        {
            customer.Property(c => c.Id)
                .HasColumnName("CustomerId")
                .IsRequired();

            customer.Property(c => c.Name)
                .HasColumnName("CustomerName")
                .IsRequired()
                .HasMaxLength(200);
        });
        
        builder.OwnsOne(s => s.Branch, branch =>
        {
            branch.Property(b => b.Id)
                .HasColumnName("BranchId")
                .IsRequired();

            branch.Property(b => b.Name)
                .HasColumnName("BranchName")
                .IsRequired()
                .HasMaxLength(200);
        });

        builder.Property(s => s.Status)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.HasMany(s => s.Items)
            .WithOne()
            .HasForeignKey("SaleId")
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Ignore(s => s.TotalAmount);
    }
}