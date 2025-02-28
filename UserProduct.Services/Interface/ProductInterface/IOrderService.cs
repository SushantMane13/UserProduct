using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Services.Models.Entity;

namespace UserProduct.Services.Interface.ProductInterface
{
    public interface IOrderService 
    {
        Task<List<Order>> GetAllOrder();

        Task<Order> GetOrderById(int id);

        Task<List<Order>> GetOrderByUserId(int id);

        Task<Order> CreateOrder(Order order);

        Task<Order> UpdateOrder(Order order);

        Task<bool> DeleteOrder(Order order);

        Task<List<Order>> DetailedOrders();
    }
}
