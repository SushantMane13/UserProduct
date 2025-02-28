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
    public class OrderService : IOrderService
    {
        private readonly UserDbContext userDbContext;

        public OrderService(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }

        public async Task<List<Order>> GetAllOrder()
        {
            return await userDbContext.Orders.ToListAsync();
        }


        public async Task<Order> GetOrderById(int id)
        {
            return await userDbContext.Orders.FindAsync(id);
        }

        public async Task<List<Order>> GetOrderByUserId(int id)
        {
            var res = userDbContext.Orders.Where(a => a.UserId == id).Include(e => e.Product).Include(a => a.Product.ProductType)
                .Include(e => e.User).Include(e => e.DelivaryType)
                .Include(e => e.PaymentMode).Include(e => e.OrderStatus);

            var orders = await res.Select(s => new Order
            {
                OrderId=s.OrderId,
                User = new User { Username = s.User.Username },
                Product = new Product { Name = s.Product.Name, ProductType = s.Product.ProductType },
                DelivaryType = new DelivaryType { Type = s.DelivaryType.Type },
                PaymentMode = new PaymentMode
                {
                    Mode = s.PaymentMode.Mode,
                    SubMode = s.PaymentMode.SubMode
                },
                OrderStatus = new OrderStatus { Status = s.OrderStatus.Status },
                PricePerUnit = s.PricePerUnit,
                Quantity = s.Quantity,
                TotalAmount = s.TotalAmount
            }).ToListAsync();
            return orders;
        }

        public async Task<Order> CreateOrder(Order order)
        {
            await userDbContext.Orders.AddAsync(order);
            await userDbContext.SaveChangesAsync();
            return order;
        }



        public async Task<Order> UpdateOrder(Order order)
        {
            userDbContext.SaveChanges();
            return order;
        }

        public async Task<bool> DeleteOrder(Order order)
        {
            userDbContext.Orders.Remove(order);
            userDbContext.SaveChanges();
            return true;
        }

        public async Task<List<Order>> DetailedOrders()
        {
            var res =  userDbContext.Orders.Include(e => e.Product).Include(a=>a.Product.ProductType)
                .Include(e => e.User).Include(e => e.DelivaryType)
                .Include(e => e.PaymentMode).Include(e => e.OrderStatus);

            var orders = await res.Select(s => new Order
            {
                User=new User{Username=s.User.Username},
                Product= new Product{Name=s.Product.Name,ProductType=s.Product.ProductType},
                DelivaryType=new DelivaryType { Type=s.DelivaryType.Type},
                PaymentMode=new PaymentMode
                {
                    Mode= s.PaymentMode.Mode,
                    SubMode=s.PaymentMode.SubMode
                },
                OrderStatus=new OrderStatus {Status=s.OrderStatus.Status},
                PricePerUnit=s.PricePerUnit,
                Quantity=s.Quantity,
                TotalAmount=s.TotalAmount
            }).ToListAsync();
            return orders;
        }
    }
}
