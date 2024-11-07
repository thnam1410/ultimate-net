namespace Catalog.Api.Infrastructure.EntityConfigurations;

public class CatalogBrandEntityConfiguration : IEntityTypeConfiguration<CatalogBrand>
{
    public void Configure(EntityTypeBuilder<CatalogBrand> builder)
    {
        builder.ToTable(nameof(CatalogBrand));

        builder.Property(cb => cb.Brand)
            .HasMaxLength(100);
    }
}