using Catalog.Api.Infrastructure;
using FluentValidation;
using MediatR;

namespace Catalog.Api.Application.UseCases.Commands;

public class CreateCatalogCommand : IRequest<CatalogItem>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CatalogTypeId { get; set; }
    public int CatalogBrandId { get; set; }

    public int? AvailableStock { get; set; }
    public int? RestockThreshold { get; set; }
    public int? MaxStockThreshold { get; set; }
}

internal sealed class CreateCatalogValidator : AbstractValidator<CreateCatalogCommand>
{
    public CreateCatalogValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(c => c.Description).NotEmpty().WithMessage("Description is required.");
        RuleFor(c => c.Price).GreaterThan(0).WithMessage("Price should be greater than 0.");
        RuleFor(c => c.CatalogTypeId).NotEmpty().GreaterThan(0).WithMessage("Catalog type is required.");
        RuleFor(c => c.CatalogBrandId).NotEmpty().GreaterThan(0).WithMessage("Catalog brand is required.");
    }
}

internal sealed class CreateCatalogCommandHandler(
    CatalogDbContext context
) : IRequestHandler<CreateCatalogCommand, CatalogItem>
{
    public async Task<CatalogItem> Handle(CreateCatalogCommand request, CancellationToken cancellationToken)
    {
        var catalog = CatalogItem.Create(
            request.Name,
            request.Description,
            request.Price,
            request.CatalogTypeId,
            request.CatalogBrandId);

        context.CatalogItems.Add(catalog);

        await context.SaveChangesAsync(cancellationToken);

        return catalog;
    }
}