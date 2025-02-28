using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Managers.DTO.ProductDTO;
using UserProduct.Managers.Interface.ProductInterface;
using UserProduct.Services.Interface.ProductInterface;
using UserProduct.Services.Models.Entity;
using UserProduct.Managers.Exceptions;

namespace UserProduct.Managers.Implmentattion.ProductClasses
{
    public class OrderStatusManager : IOrderStatusManager
    {
        private readonly IOrderStatusService orderStatusService;
       
        public OrderStatusManager(IOrderStatusService orderStatusService)
        {
            this.orderStatusService = orderStatusService;
        }

        public async Task<List<OrderStatusDTO>> GetAllOrderStatus()
        {
            var res = await orderStatusService.GetAllOrderStatus();
            return res.Select(x => OrderStatusDTO.MapToOrderStatusDTO(x)).ToList();
        }

        public async Task<OrderStatusDTO> GetOrderStatusById(int id)
        {
            List<string> exception = [];
            if (id <= 0)
                exception.Add("Enter Valid Details");

            var res = await orderStatusService.GetOrderStatusById(id);
            if (res == null)
                exception.Add("Order Status Does Not Exist");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            return OrderStatusDTO.MapToOrderStatusDTO(res);
        }

        public async Task<OrderStatusDTO> CreateOrderStatus(OrderStatusDTO orderStatusDTO)
        {
            List<string> exception = [];
            if (string.IsNullOrEmpty(orderStatusDTO.Status.Trim()))
                exception.Add("Enter Valid Details");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            var orderStatus = OrderStatusDTO.MapToOrderStatus(orderStatusDTO);
            var res = await orderStatusService.CreateOrderStatus(orderStatus);
            return OrderStatusDTO.MapToOrderStatusDTO(res);
        }

        public async Task<OrderStatusDTO> UpdateOrderStatus(int id, OrderStatusDTO orderStatusDTO)
        {
            List<string> exception = [];
            if ( string.IsNullOrEmpty(orderStatusDTO.Status.Trim() )|| id<=0)
                exception.Add("Enter Valid Details");


            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            var orderStatus = await orderStatusService.GetOrderStatusById(id);
            if (orderStatus == null)
                exception.Add("Order Status Does Not Exist");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            orderStatus.Status = orderStatusDTO.Status;

            var res = await orderStatusService.UpdateOrderStatus(orderStatus);

            return OrderStatusDTO.MapToOrderStatusDTO(res);
        }

        public async Task<bool> DeleteOrderStatus(int id)
        {
            List<string> exception = [];
            if (id <= 0)
                exception.Add("Enter Valid Details");

            var orderStatus = await orderStatusService.GetOrderStatusById(id);
            if (orderStatus == null)
                exception.Add("Order Status Does Not Exist");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            bool res = await orderStatusService.DeleteOrderStatus(orderStatus);
            return res;
        }
    }
}
