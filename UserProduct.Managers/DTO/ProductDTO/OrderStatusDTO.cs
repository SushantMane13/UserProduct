

using UserProduct.Services.Models.Entity;

namespace UserProduct.Managers.DTO.ProductDTO
{
    public class OrderStatusDTO
    {
        public int OrderStatusId { get; set; }
        public string Status { get; set; }

        public static OrderStatus MapToOrderStatus(OrderStatusDTO orderStatusDTO)
        {
            return new OrderStatus
            {
                Status = orderStatusDTO.Status
            };
        }
        public static OrderStatusDTO MapToOrderStatusDTO(OrderStatus orderStatus)
        {
            return new OrderStatusDTO
            {
                OrderStatusId=orderStatus.OrderStatusId,
                Status = orderStatus.Status
            };
        }
    }
}
