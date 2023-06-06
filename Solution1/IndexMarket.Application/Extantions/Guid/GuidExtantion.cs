using IndexMarket.Domain.Entities;
using IndexMarket.Domain.Exceptions;

namespace IndexMarket.Application.Extantions;
public static class GuidExtantion
{
    public static void IsDefault(this Guid guid)
    {
        if(guid.Equals(default(Guid)))
        {
            throw new ValidationException($"The given Id is invalid: {guid}");
        }
    }
}
