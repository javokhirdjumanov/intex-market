using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace IndexMarket.Infrastructure.Repository.OrdersRepositories;
[Keyless]
public class report_model
{
    [Column("product_id")]
    public Guid ProductId { get; set; }

    [Column("category_name")]
    public string CategoryName { get; set; }

    [Column("quantity")]
    public long Quantity { get; set; }
}
