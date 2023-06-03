﻿using IndexMarket.Domain.Entities;

namespace IndexMarket.Infrastructure.Repository;
public interface IProductShapeRepository : IBaseRepository<ProductShape, Guid>
{
    Task<ProductShape> GetShapeByNameAsync(string shapeName);
}
