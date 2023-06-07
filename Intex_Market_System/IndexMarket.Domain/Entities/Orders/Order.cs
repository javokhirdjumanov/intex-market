namespace IndexMarket.Domain.Entities;
public class Order : AudiTable
{
    public Guid Client_Id { get; set; }
    public User User { get; set;}

    public Guid Product_Id { get; set; }
    public Product Product { get; set; }
}
