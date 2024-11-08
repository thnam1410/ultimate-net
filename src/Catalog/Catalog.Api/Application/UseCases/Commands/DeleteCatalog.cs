using Catalog.Api.Infrastructure;
using MediatR;
using UltimateNet.Shared.Exceptions;

namespace Catalog.Api.Application.UseCases.Commands;

public record DeleteCatalogCommand(int Id) : IRequest;

internal sealed class DeleteCatalogCommandHandler(
    CatalogDbContext dbContext
    ): IRequestHandler<DeleteCatalogCommand>
{
    public async Task Handle(DeleteCatalogCommand request, CancellationToken cancellationToken)
    {
        var item = await dbContext.CatalogItems.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        
        if (item is null)
            throw new EntityNotFoundException<int>(nameof(CatalogItem), request.Id);
        
        dbContext.CatalogItems.Remove(item);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
