using Catalog.Api.Application.UseCases.Commands;
using MediatR;

namespace Catalog.Api.Apis;

public class CatalogMutation
{
/**
mutation CreateCatalog($command: CreateCatalogCommandInput!) {
  addCatalog(command: $command){
    id
    name
  }
}
--
{
  "command": {
    "name": "Test",
    "description": "Test Description",
    "price": 100,
    "catalogTypeId": 1,
    "catalogBrandId": 1
  }
}
*/
    public async Task<CatalogItem> AddCatalog([Service]ISender sender, CreateCatalogCommand command)
    {
        return await sender.Send(command);
    }

/**
mutation UpdateCatalog ($command: UpdateCatalogCommandInput!){
  updateCatalog(command: $command){
    id
    name
  }
}
--
{
  "command": {
    "id": 103,
    "name": "Test1",
    "description": "Description1",
    "price": 200,
    "catalogBrandId": 1,
    "catalogTypeId": 1,
    "availableStock": 100,
    "restockThreshold": 200,
    "maxStockThreshold": 10
}
*/
  public async Task<CatalogItem> UpdateCatalog([Service]ISender sender, UpdateCatalogCommand command)
  {
      return await sender.Send(command);
  }
/**
mutation DeleteCatalog($id: Int!) {
  deleteCatalog(id: $id)
}
--
{
  "id": 103
}

 */
  public async Task<bool> DeleteCatalog([Service]ISender sender, int id)
  {
    await sender.Send(new DeleteCatalogCommand(id));
    return true;
  }
}