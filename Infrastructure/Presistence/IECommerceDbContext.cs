using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Presistence
{
    public interface IECommerceDbContext
    {
        DbSet<OrderItem> OrderItems { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Stock> Stocks { get; set; }
    }
}