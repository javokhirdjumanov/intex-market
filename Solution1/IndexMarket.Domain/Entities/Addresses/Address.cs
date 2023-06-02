namespace IndexMarket.Domain.Entities;
public class Address
{
    public Guid Id { get; set; }
    public string Country { get; set; }
    public string? City { get; set; }
    public string? Region { get; set; }
    public string? Street { get; set; }
    public short? PostalCode{ get; set; }
}
