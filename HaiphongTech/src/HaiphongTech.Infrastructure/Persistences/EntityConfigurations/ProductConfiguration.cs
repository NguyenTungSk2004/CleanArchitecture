using HaiphongTech.Domain.Entities.Products.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HaiphongTech.Infrastructure.Persistences.EntityConfigurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Code).IsUnique();
        builder.HasIndex(x => x.BarCode).IsUnique();

        builder.Property(x => x.Code).IsRequired().HasColumnType("varchar(200)");
        builder.Property(x => x.BarCode).HasColumnType("varchar(200)");
        builder.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(300)");

        builder.Property(x => x.CostPrice).HasColumnType("decimal(18,2)");

        builder.OwnsOne(x => x.PriceTier1, priceTier =>
        {
            priceTier.Property(p => p.Quantity).HasColumnType("int").HasColumnName("Quantity1").HasColumnOrder(5);
            priceTier.Property(p => p.Price).HasColumnType("decimal(18,2)").HasColumnName("SellingPrice1").HasColumnOrder(6);
        });
        builder.OwnsOne(x => x.PriceTier2, priceTier =>
        {
            priceTier.Property(p => p.Quantity).HasColumnType("int").HasColumnName("Quantity2").HasColumnOrder(7);
            priceTier.Property(p => p.Price).HasColumnType("decimal(18,2)").HasColumnName("SellingPrice2").HasColumnOrder(8);
        });
        builder.OwnsOne(x => x.PriceTier3, priceTier =>
        {
            priceTier.Property(p => p.Quantity).HasColumnType("int").HasColumnName("Quantity3").HasColumnOrder(9);
            priceTier.Property(p => p.Price).HasColumnType("decimal(18,2)").HasColumnName("SellingPrice3").HasColumnOrder(10);
        });
        builder.OwnsOne(x => x.PriceTier4, priceTier =>
        {
            priceTier.Property(p => p.Quantity).HasColumnType("int").HasColumnName("Quantity4").HasColumnOrder(11);
            priceTier.Property(p => p.Price).HasColumnType("decimal(18,2)").HasColumnName("SellingPrice4").HasColumnOrder(12);
        });
        builder.OwnsOne(x => x.PreOrderInfo, preOrder =>
        {
            preOrder.Property(p => p.WaitingTimeUntilAvailable).HasColumnType("nvarchar(300)").HasColumnName("WaitingTimeUntilAvailable");
            preOrder.Property(p => p.Quantity).HasColumnType("int").HasColumnName("PreOrderQuantity");
        });
    }
}