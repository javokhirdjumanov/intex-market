using System.Runtime.CompilerServices;

namespace IndexMarket.Domain.Entities;
public class ProductShape
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public ICollection<Product> Products { get; set; }
}
