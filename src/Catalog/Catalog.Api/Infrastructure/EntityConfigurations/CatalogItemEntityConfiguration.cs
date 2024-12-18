﻿namespace Catalog.Api.Infrastructure.EntityConfigurations;

public class CatalogItemEntityConfiguration : IEntityTypeConfiguration<CatalogItem>
{
    public void Configure(EntityTypeBuilder<CatalogItem> builder)
    {
        builder.ToTable(nameof(CatalogItem));

        builder.Property(ci => ci.Name)
            .HasMaxLength(50);

        builder.HasOne(ci => ci.CatalogBrand)
            .WithMany();

        builder.HasOne(ci => ci.CatalogType)
            .WithMany();

        builder.HasIndex(ci => ci.Name);
    }
}