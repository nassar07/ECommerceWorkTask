using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.Order;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IOrderRepository
    {
        Task CreateOrderAsync(Order order);
        Task<List<Order>> GetAllOrdersAsync();
        Task<List<Order>> GetOrdersByClientIdAsync(string clientId);
        Task<Order?> GetOrderByIdAsync(int orderId);
        Task<bool> MarkOrderAsShippedAsync(int orderId);

        Task SaveChangesAsync();
    }
}
