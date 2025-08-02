using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Commands_Queries.Order.PlaceOrder.Commands;
using Application.DTO.Order;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Common.Interfaces
{
    public interface IOrderRepository
    {
        Task CreateOrderAsync(Order order);
        Task<List<Order>> GetAllOrdersAsync();
        Task<List<Order>> GetOrdersByClientIdAsync(string clientId);
        Task<Order?> GetOrderByIdAsync(int orderId);
        Task<bool> MarkOrderAsShippedAsync(int orderId);
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task<ProductSize?> GetProductSizeByIdAsync(int productSizeId, CancellationToken cancellationToken);
        Task SaveChangesAsync();
    }
}
