using IndexMarket.Domain.Enums;

namespace IndexMarket.Domain.Entities;
public class Product : AudiTable
{
    public string? PhotoLink { get; set; }
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public string Frame { get; set; }
    public double Height { get; set; }
    public double? Weight { get; set; }

    public Guid Category_Id { get; set; }
    public Category Category { get; set; }

    public ICollection<Order> Orders { get; set; }
    public ProductType Type { get; set; }
}
