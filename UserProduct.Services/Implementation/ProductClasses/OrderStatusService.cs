using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserProduct.Services.Interface.ProductInterface;
using UserProduct.Services.Models.Entity;
using UserProduct.Services.Models;

namespace UserProduct.Services.Implementation.ProductClasses
{
    public class OrderStatusService : IOrderStatusService
    {
        private readonly UserDbContext userDbContext;

        public OrderStatusService(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }

        public async Task<List<OrderStatus>> GetAllOrderStatus()
        {
            var temp = await userDbContext.OrderStatuses.Select(x => new OrderStatus()
            {
                OrderStatusId = x.OrderStatusId,
                Orders = x.Orders.Select(x => new Order()
                {
                    
                }).ToList(),
            }).ToListAsync();
            return await userDbContext.OrderStatuses.ToListAsync();
        }


        public async Task<OrderStatus> GetOrderStatusById(int id)
        {
            return await userDbContext.OrderStatuses.FindAsync(id);
        }

        public async Task<OrderStatus> CreateOrderStatus(OrderStatus orderStatus)
        {
            await userDbContext.OrderStatuses.AddAsync(orderStatus);
            await userDbContext.SaveChangesAsync();
            return orderStatus;
        }

        public async Task<OrderStatus> UpdateOrderStatus(OrderStatus orderStatus)
        {
            userDbContext.SaveChanges();
            return orderStatus;
        }

        public async Task<bool> DeleteOrderStatus(OrderStatus orderStatus)
        {
            userDbContext.OrderStatuses.Remove(orderStatus);
            userDbContext.SaveChanges();
            return true;
        }
    }
}
