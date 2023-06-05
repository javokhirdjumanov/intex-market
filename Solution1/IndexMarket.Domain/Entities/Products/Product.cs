using IndexMarket.Domain.Enums;

namespace IndexMarket.Domain.Entities;
public class Product : AudiTable
{
    public string? PhotoLink { get; set; }
    public decimal? SalePrice { get; set; }
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public double Height { get; set; }
    public double? Weight { get; set; }
    public int Depth { get; set; }
    public ProductStatus Status { get; set; }

    public Guid File_Id { get; set; }
    public FileModel File { get; set; }

    public Guid Category_Id { get; set; }
    public Category Category { get; set; }

    public Guid Shape_Id { get; set; }
    public ProductShape ProductShape { get; set; }

    public ICollection<Order>? Orders { get; set; }

    //public ProductType Type { get; set; }
    public void ProductNotAvailable() => Status = ProductStatus.Not_Available; // mavjud Emas
    public void ProductRecommended() => Status = ProductStatus.Recommended; // tafsiya Etiladi
    public void ProductDiscount() => Status = ProductStatus.Discount; // skidka
}
