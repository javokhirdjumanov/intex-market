namespace IndexMarket.Domain.Entities;

public class Site : AudiTable
{
    public string PhoneNumber { get; set; }
    public string JobTime { get; set; }
    public string TelegrammLink { get; set; }
    public string InstagramLink { get; set; }

    public Guid Address_Id { get; set; }
    public Address Address { get; set; }
}
