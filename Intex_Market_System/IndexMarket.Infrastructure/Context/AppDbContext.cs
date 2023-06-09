using IndexMarket.Domain.Entities;
using IndexMarket.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IndexMarket.Infrastructure.Context;
public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
