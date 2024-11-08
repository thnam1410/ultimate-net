using Catalog.Api.Infrastructure;

namespace Catalog.Api.Apis;

public class CatalogQuery
{
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<CatalogItem> GetCatalogItems(
        [Service] CatalogDbContext dbContext
    ) => dbContext.CatalogItems.AsQueryable();
    
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<CatalogType> GetCatalogTypes(
        [Service] CatalogDbContext dbContext
    ) => dbContext.CatalogTypes.AsQueryable();
    
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<CatalogBrand> GetCatalogBrand(
        [Service] CatalogDbContext dbContext
    ) => dbContext.CatalogBrands.AsQueryable();
}