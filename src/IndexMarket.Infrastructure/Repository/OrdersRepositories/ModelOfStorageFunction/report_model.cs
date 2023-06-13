using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace IndexMarket.Infrastructure.Repository.OrdersRepositories;
[Keyless]
public class report_model
{
    [Column("id")]
    public Guid Id { get; set; }

    [Column("category_name")]
    public string CategoryName { get; set; }

    [Column("quentity")]
    public long Quantity { get; set; }
}
