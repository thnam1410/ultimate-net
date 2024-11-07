using System.Collections.Generic;

namespace UltimateNet.Domain.Shared;

public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = [];

    [GraphQLIgnore]
    public List<IDomainEvent> DomainEvents => [.. _domainEvents];

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}