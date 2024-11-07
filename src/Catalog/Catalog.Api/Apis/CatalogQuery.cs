using Catalog.Api.Infrastructure;

namespace Catalog.Api.Apis;

public class CatalogQuery
{
    // public async Task<List<CatalogItemDto>> GetCatalogItemsAsync(
    //     [Service] ISender sender
    // ) => await sender.Send(new GetListCatalogItemQuery());

    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<CatalogItem> GetCatalogItems(
        [Service] CatalogDbContext dbContext
    ) => dbContext.CatalogItems.AsQueryable();
}