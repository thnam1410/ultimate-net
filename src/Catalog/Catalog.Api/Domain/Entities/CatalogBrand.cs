using System.ComponentModel.DataAnnotations;

namespace Catalog.Api.Domain.Entities;

public class CatalogBrand
{
    public int Id { get; set; }

    [Required]
    public string Brand { get; set; }
}