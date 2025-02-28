

using UserProduct.Services.Models.Entity;

namespace UserProduct.Managers.DTO.ProductDTO
{
    public class OrderDTO
    {
        public int ProductId { get; set; }

        public int UserId { get; set; }

        public int Quantity { get; set; }

        public int PricePerUnit { get; set; }

        public int? TotalAmount { get; set; }

        public int DelivaryTypeId { get; set; }

        public int PaymentModeId { get; set; }

        public int OrderStatusId { get; set; }

        public static OrderDTO MapToOrderDTO(Order order)
        {
            return new OrderDTO
            {
                ProductId = order.ProductId,
                UserId = order.UserId,
                Quantity = order.Quantity,
                PricePerUnit = order.PricePerUnit,
                TotalAmount = order.TotalAmount,
                DelivaryTypeId = order.DelivaryTypeId,
                PaymentModeId = order.PaymentModeId,
                OrderStatusId = order.OrderStatusId
            };
        }

        public static Order MapToOrder(OrderDTO orderDTO)
        {
            return new Order
            {
                ProductId = orderDTO.ProductId,
                UserId = orderDTO.UserId,
                Quantity = orderDTO.Quantity,
                PricePerUnit = orderDTO.PricePerUnit,
                TotalAmount = orderDTO.TotalAmount,
                DelivaryTypeId = orderDTO.DelivaryTypeId,
                PaymentModeId = orderDTO.PaymentModeId,
                OrderStatusId = orderDTO.OrderStatusId
            };
        }
    }
}
