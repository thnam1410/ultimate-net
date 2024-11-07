using Catalog.Api.Infrastructure.EntityConfigurations;

namespace Catalog.Api.Infrastructure;

public class CatalogDbContext: DbContext
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options, IConfiguration configuration) : base(options)
    {
    }
    
    public DbSet<CatalogItem> CatalogItems { get; set; }
    public DbSet<CatalogBrand> CatalogBrands { get; set; }
    public DbSet<CatalogType> CatalogTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new CatalogItemEntityConfiguration());
        builder.ApplyConfiguration(new CatalogTypeEntityConfiguration());
        builder.ApplyConfiguration(new CatalogBrandEntityConfiguration());
        
        //TODO: Outbox pattern with MassTransit
    }
}