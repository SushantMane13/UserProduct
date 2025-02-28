using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Managers.DTO.ProductDTO;
using UserProduct.Managers.Interface.ProductInterface;
using UserProduct.Services.Interface.ProductInterface;
using UserProduct.Services.Interface.UserInterfaces;
using UserProduct.Managers.Exceptions;

namespace UserProduct.Managers.Implmentattion.ProductClasses
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly IUserService   userService;
        private readonly IDelivaryTypeServices delivaryTypeServices;
        private readonly IPaymentModeService paymentModeService;
        private readonly IOrderStatusService orderStatusService;
        
        public OrderManager(IOrderService orderService,IProductService productService,IUserService userService, IDelivaryTypeServices delivaryTypeServices, IPaymentModeService paymentModeService, IOrderStatusService orderStatusService)
        {
            this.orderService = orderService;
            this.productService = productService;
            this.userService = userService;
            this.delivaryTypeServices = delivaryTypeServices;
            this.paymentModeService = paymentModeService;
            this.orderStatusService = orderStatusService;
        }

        public async Task<List<OrderDTO>> GetAllOrder()
        {
            var res = await orderService.GetAllOrder();
            return res.Select(x => OrderDTO.MapToOrderDTO(x)).ToList();
        }

        public async Task<List<DetailedOrderDTO>> GetDetailedOrder()
        {
            var res=await orderService.DetailedOrders();
            return res.Select(s=>DetailedOrderDTO.MapToDTO(s)).ToList();
        }

        public async Task<OrderDTO> GetOrderById(int id)
        {
            List<string> exception = [];
            if (id <= 0)
                exception.Add("Enter Valid Details");

            var res = await orderService.GetOrderById(id);
            if (res == null)
                exception.Add("Order Does Not Exist");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            return OrderDTO.MapToOrderDTO(res);
        }

        public async Task<List<DetailedOrderDTO>> GetOrderByUserId(int id)
        {
            List<string> exception = [];
            if (id <= 0)
                exception.Add("Enter Valid Details");

            var res = await orderService.GetOrderByUserId(id);
            if (res == null)
                exception.Add("Order Does Not Exist");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            return res.Select(s => DetailedOrderDTO.MapToDTO(s)).ToList();
        }

        public async Task<OrderDTO> CreateOrder(OrderDTO orderDTO)
        {
            List<string> exception = [];
            if ( orderDTO.ProductId <=0 || orderDTO.UserId<=0 || orderDTO.Quantity <= 0 || orderDTO.PricePerUnit <= 0 || orderDTO.TotalAmount != 0 ||
                orderDTO.DelivaryTypeId <= 0 || orderDTO.PaymentModeId <= 0 || orderDTO.OrderStatusId <= 0)
                exception.Add("Enter Valid Details");

            if (await productService.GetProductById(orderDTO.ProductId) == null)
                exception.Add("Product Does Not Exist");

            if (await userService.GetUserById(orderDTO.UserId) == null)
                exception.Add("User Does Not Exist");

            if (await delivaryTypeServices.GetDelivaryTypeById(orderDTO.DelivaryTypeId) == null)
                exception.Add("Delivery Type Does Not Exist");

            if (await paymentModeService.GetPaymentModeById(orderDTO.PaymentModeId) == null)
                exception.Add("Payment Mode Does Not Exist");

            if (await orderStatusService.GetOrderStatusById(orderDTO.OrderStatusId) == null)
                exception.Add("Order Status Does Not Exist");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            var order = OrderDTO.MapToOrder(orderDTO);
            var res = await orderService.CreateOrder(order);
            return OrderDTO.MapToOrderDTO(res);
        }

        public async Task<OrderDTO> UpdateOrder(int id, OrderDTO orderDTO)
        {
            List<string> exception = [];
            if ( orderDTO.Quantity <= 0 || orderDTO.PricePerUnit <= 0 || orderDTO.TotalAmount != 0)
                exception.Add("Enter Valid Details");

            if (await productService.GetProductById(orderDTO.ProductId) == null)
                exception.Add("Product Does Not Exist");

            if (await userService.GetUserById(orderDTO.UserId) == null)
                exception.Add("User Does Not Exist");

            if (await delivaryTypeServices.GetDelivaryTypeById(orderDTO.DelivaryTypeId) == null)
                exception.Add("Delivery Type Does Not Exist");

            if (await paymentModeService.GetPaymentModeById(orderDTO.PaymentModeId) == null)
                exception.Add("Payment Mode Does Not Exist");

            if (await orderStatusService.GetOrderStatusById(orderDTO.OrderStatusId) == null)
                exception.Add("Order Status Does Not Exist");

            var order = await orderService.GetOrderById(id);
            if (order == null)
                exception.Add("Order is not presnet");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            order.ProductId = orderDTO.ProductId;
            order.UserId = orderDTO.UserId;
            order.Quantity = orderDTO.Quantity;
            order.PricePerUnit = orderDTO.PricePerUnit;
            order.TotalAmount = orderDTO.TotalAmount;
            order.DelivaryTypeId = orderDTO.DelivaryTypeId;
            order.PaymentModeId = orderDTO.PaymentModeId;
            order.OrderStatusId = orderDTO.OrderStatusId;

            var res = await orderService.UpdateOrder(order);

            return OrderDTO.MapToOrderDTO(res);
        }

        public async Task<bool> DeleteOrder(int id)
        {
            List<string> exception = [];
            if (id <= 0)
                exception.Add("Enter Valid Details");

            var order = await orderService.GetOrderById(id);
            if (order == null)
                exception.Add("Order Does Not Exist");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            bool res = await orderService.DeleteOrder(order);
            return res;
        }
    }
}
