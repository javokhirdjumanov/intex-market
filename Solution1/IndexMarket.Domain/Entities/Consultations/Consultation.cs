namespace IndexMarket.Domain.Entities;
public class Consultation
{
    public Guid Id { get; set; }
    public Guid Order_Id { get; set; }
    public Order Order{ get; set; }
}
