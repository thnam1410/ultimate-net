namespace Catalog.Api.Contracts;

public class CatalogItemDto
{
    public int Id { get; init; }

    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public decimal Price { get; set; }
    
    public int CatalogTypeId { get; set; }
    
    public int CatalogBrandId { get; set; }
    
    public int AvailableStock { get; set; }

    public int RestockThreshold { get; set; }

    public int MaxStockThreshold { get; set; }
}