using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Managers.DTO.ProductDTO;

namespace UserProduct.Managers.Interface.ProductInterface
{
    public interface IOrderStatusManager
    {
        Task<List<OrderStatusDTO>> GetAllOrderStatus();

        Task<OrderStatusDTO> GetOrderStatusById(int id);

        Task<OrderStatusDTO> CreateOrderStatus(OrderStatusDTO orderStatusDTO);

        Task<OrderStatusDTO> UpdateOrderStatus(int id, OrderStatusDTO orderStatusDTO);

        Task<bool> DeleteOrderStatus(int id);
    }
}
