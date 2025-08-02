using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.DTO.Order;
using Domain.Entities;
using Infrastructure.Presistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public OrderRepository(ECommerceDbContext context)
        {
            Context = context;
        }

        public ECommerceDbContext Context { get; }

        public async Task CreateOrderAsync(Order order)
        {
            await Context.Set<Order>().AddAsync(order);
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return await Context.Database.BeginTransactionAsync(cancellationToken);
        }
        public async Task<ProductSize?> GetProductSizeByIdAsync(int productSizeId, CancellationToken cancellationToken)
        {
            return await Context.Set<ProductSize>().FindAsync(new object[] { productSizeId }, cancellationToken);
        }

        public async Task<List<Order>> GetOrdersByClientIdAsync(string clientId)
        {
            return await Context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.ProductSize)
                        .ThenInclude(ps => ps.Product)
                .Where(o => o.ClientId == clientId)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await Context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.ProductSize)
                        .ThenInclude(ps => ps.Product)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }
        public async Task<Order?> GetOrderByIdAsync(int orderId)
{
    return await Context.Orders
        .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.ProductSize)
                .ThenInclude(ps => ps.Product)
        .FirstOrDefaultAsync(o => o.Id == orderId);
}

        public async Task<bool> MarkOrderAsShippedAsync(int orderId)
        {
            var order = await Context.Orders.FindAsync(orderId);
            if (order == null) return false;

            order.IsShipped = true;
            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Order>> GetOrdersWithPaginationAsync(int pageNumber, int pageSize)
        {
            return await Context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.ProductSize)
                        .ThenInclude(ps => ps.Product)
                .OrderByDescending(o => o.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public Task SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }
    }
}
