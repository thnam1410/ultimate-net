using System.ComponentModel.DataAnnotations;
using UltimateNet.Domain.Shared;

namespace Catalog.Api.Domain.Entities;

public sealed class CatalogItem : Entity
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    
    public decimal Price { get; set; }
    
    public int CatalogTypeId { get; set; }

    public CatalogType CatalogType { get; set; }
    
    public int CatalogBrandId { get; set; }

    public CatalogBrand CatalogBrand { get; set; }
    
    // Quantity in stock
    public int AvailableStock { get; set; }

    // Available stock at which we should reorder
    public int RestockThreshold { get; set; }


    // Maximum number of units that can be in-stock at any time (due to physicial/logistical constraints in warehouses)
    public int MaxStockThreshold { get; set; }
}