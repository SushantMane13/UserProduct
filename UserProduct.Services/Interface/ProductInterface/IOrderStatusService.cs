using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Services.Models.Entity;

namespace UserProduct.Services.Interface.ProductInterface
{
    public interface IOrderStatusService
    {
        Task<List<OrderStatus>> GetAllOrderStatus();

        Task<OrderStatus> GetOrderStatusById(int id);

        Task<OrderStatus> CreateOrderStatus(OrderStatus orderStatus);

        Task<OrderStatus> UpdateOrderStatus(OrderStatus orderStatus);

        Task<bool> DeleteOrderStatus(OrderStatus orderStatus);
    }
}
