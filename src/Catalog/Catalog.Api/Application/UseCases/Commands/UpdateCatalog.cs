using Catalog.Api.Infrastructure;
using FluentValidation;
using MediatR;
using UltimateNet.Shared.Exceptions;

namespace Catalog.Api.Application.UseCases.Commands;

public class UpdateCatalogCommand : IRequest<CatalogItem>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CatalogTypeId { get; set; }
    public int CatalogBrandId { get; set; }

    public int AvailableStock { get; set; }
    public int RestockThreshold { get; set; }
    public int MaxStockThreshold { get; set; }
}

internal sealed class UpdateCatalogValidator : AbstractValidator<UpdateCatalogCommand>
{
    public UpdateCatalogValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
    }
}

internal sealed class UpdateCatalogCommandHandler(
    CatalogDbContext context
) : IRequestHandler<UpdateCatalogCommand, CatalogItem>
{
    public async Task<CatalogItem> Handle(UpdateCatalogCommand request, CancellationToken cancellationToken)
    {
        var catalogItem = await context.CatalogItems.SingleOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (catalogItem is null)
        {
            throw new EntityNotFoundException<int>(nameof(CatalogItem), request.Id);
        }

        // Update current product
        var catalogEntry = context.Entry(catalogItem);
        catalogEntry.CurrentValues.SetValues(request);
        
        var priceEntry = catalogEntry.Property(i => i.Price);

        if (priceEntry.IsModified)
        {
            // Raise events
        }
        
        await context.SaveChangesAsync(cancellationToken);
        
        return catalogItem;
    }
}