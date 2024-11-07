namespace Catalog.Api.Infrastructure.EntityConfigurations;

public class CatalogTypeEntityConfiguration : IEntityTypeConfiguration<CatalogType>
{
    public void Configure(EntityTypeBuilder<CatalogType> builder)
    {
        builder.ToTable(nameof(CatalogType));

        builder.Property(cb => cb.Type)
            .HasMaxLength(100);
    }
}