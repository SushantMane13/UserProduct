using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Managers.DTO.ProductDTO;


namespace UserProduct.Managers.Interface.ProductInterface
{
    public interface IOrderManager
    {
        Task<List<OrderDTO>> GetAllOrder();

        Task<OrderDTO> GetOrderById(int id);

        Task<List<DetailedOrderDTO>> GetOrderByUserId(int id);

        Task<OrderDTO> CreateOrder(OrderDTO orderDTO);

        Task<OrderDTO> UpdateOrder(int id, OrderDTO orderDTO);

        Task<bool> DeleteOrder(int id);

        Task<List<DetailedOrderDTO>> GetDetailedOrder();
    }
}
