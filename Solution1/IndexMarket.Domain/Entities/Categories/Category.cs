using System.Security.Principal;

namespace IndexMarket.Domain.Entities;
public class Category : AudiTable
{
    public string Title { get; set; }

    public ICollection<Product> Products { get; set; }
}
