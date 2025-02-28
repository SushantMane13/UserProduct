using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Services.Models.Entity;

namespace UserProduct.Managers.DTO.ProductDTO
{
    public class DetailedOrderDTO
    {
        public int OrderId { get; set; }
        public string Username { get; set; }

        public string ProductName { get; set; }

        public string ProductType { get; set; }

        public int Price { get; set; }

        public int OrderedQuantity { get; set; }

        public int TotalAmount { get; set; }

        public string DeliveryType { get; set; }

        public string PaymentMode { get; set; }

        public string DeliveryStatus { get; set; }

        public static DetailedOrderDTO MapToDTO(Order order)
        {
            return new DetailedOrderDTO
            {
                OrderId = order.OrderId,    
                Username = order.User.Username,
                ProductName = order.Product.Name,
                ProductType= order.Product.ProductType.TypeName,
                Price = order.PricePerUnit,
                OrderedQuantity = order.Quantity,
                TotalAmount =(int) order.TotalAmount,
                DeliveryType = order.DelivaryType.Type,
                PaymentMode = order.PaymentMode.Mode + " : " + order.PaymentMode.SubMode,
               DeliveryStatus = order.OrderStatus.Status
            };
        }
    }
}
