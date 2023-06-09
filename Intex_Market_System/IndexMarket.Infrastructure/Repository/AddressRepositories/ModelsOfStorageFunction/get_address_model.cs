using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace IndexMarket.Infrastructure.Repository;
[Keyless]
public class get_address_model
{
    [Column("iid")]
    public Guid Id { get; set; }

    [Column("country")]
    public string Country { get; set; }

    [Column("city")]
    public string? City { get; set; }

    [Column("region")]
    public string? Region { get; set; }

    [Column("street")]
    public string? Street { get; set; }

    [Column("postal_code")]
    public short? PostalCode { get; set; }
}
