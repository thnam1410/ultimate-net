using System;

namespace UltimateNet.Shared.Exceptions;

public class EntityNotFoundException<TId> : Exception
{
    public string EntityName { get; }
    public TId Key { get; }

    public EntityNotFoundException(string entityName, TId key)
        : base($"Entity '{entityName}' with key '{key}' was not found.")
    {
        EntityName = entityName;
        Key = key;
    }
}