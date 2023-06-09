using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace IndexMarket.Infrastructure.Repository.OrdersRepositories;

[Keyless]
public class filter_products_price_in_order_model
{
    [Column("order_id")]
    public Guid order_id { get; set; }

    [Column("user_name")]
    public string user_name { get; set; }

    [Column("file_id")]
    public Guid file_id { get; set; }

    [Column("file_type")]
    public string file_type { get; set; }

    [Column("file_name")]
    public string file_name { get; set;}

    [Column("phone_number")]
    public string phone_number { get; set; }

    [Column("height")]
    public double height { get; set; }

    [Column("weight")]
    public double? weight { get; set; }

    [Column("depths")]
    public int depth { get; set; }

    [Column("price")]
    public decimal price { get; set; }

    [Column("address_id")]
    public Guid address_id { get; set; }

    [Column("country")]
    public string country { get; set; }

    [Column("city")]
    public string? city { get; set; }

    [Column("region")]
    public string? region { get; set; }

    [Column("street")]
    public string? street { get; set; }

    [Column("postal_code")]
    public short? postal_code { get; set; }

    [Column("create_at")]
    public DateTime create_at { get; set; }
}
