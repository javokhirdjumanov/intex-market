using IndexMarket.Domain.Exceptions;

namespace IndexMarket.Application.Services;
public static class ValidationStorageObject
{
    public static void ValidationGeneric<TEntity>(TEntity entity, Guid id)
    {
        if (entity is null)
        {
            throw new NotFoundException($"Couldn't find object with given id: {id}");
        }
    }
}
