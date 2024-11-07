using Catalog.Api.Contracts;

namespace Catalog.Api.Apis.Mapping;

public static class DomainToDtoMapper
{
    public static CatalogItemDto MapToCatalogItemDto(this CatalogItem catalogItem)
    {
        return new CatalogItemDto
        {
            Id = catalogItem.Id,
            Name = catalogItem.Name,
            Description = catalogItem.Description,
            Price = catalogItem.Price,
            CatalogBrandId = catalogItem.CatalogBrandId,
            CatalogTypeId = catalogItem.CatalogTypeId
        };
    }
}