using Domain.ProductModule.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistences.EntityConfigurations;

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

        builder.OwnsMany(p => p.PriceTiers, pb =>
        {
            pb.ToTable("ProductPriceTiers");
            pb.WithOwner().HasForeignKey("ProductId");
            pb.Property(p => p.Price);
            pb.Property(p => p.Quantity);
        });
        builder.OwnsOne(x => x.PreOrderInfo, preOrder =>
        {
            preOrder.Property(p => p.WaitingTimeUntilAvailable).HasColumnType("nvarchar(300)").HasColumnName("WaitingTimeUntilAvailable");
            preOrder.Property(p => p.Quantity).HasColumnType("int").HasColumnName("PreOrderQuantity");
        });
    }
}