using Catalog.Api.Apis.Mapping;
using Catalog.Api.Contracts;
using Catalog.Api.Infrastructure;
using MediatR;

namespace Catalog.Api.Application.UseCases.Queries;

public record GetListCatalogItemQuery : IRequest<List<CatalogItemDto>>
{
}

internal class GetListCatalogItemQueryHandler(
    CatalogDbContext dbContext
) : IRequestHandler<GetListCatalogItemQuery, List<CatalogItemDto>>
{
    public async Task<List<CatalogItemDto>> Handle(GetListCatalogItemQuery request, CancellationToken cancellationToken)
    {
        return await dbContext
            .CatalogItems
            .Select(c => c.MapToCatalogItemDto())
            .ToListAsync(cancellationToken);
    }
}