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
    public class PaymentModeService : IPaymentModeService
    {
        private readonly UserDbContext userDbContext;

        public PaymentModeService(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }

        public async Task<List<PaymentMode>> GetAllPaymentModes()
        {
            return await userDbContext.PaymentModes.ToListAsync();
        }


        public async Task<PaymentMode> GetPaymentModeById(int id)
        {
            return await userDbContext.PaymentModes.FindAsync(id);
        }

        public async Task<PaymentMode> CreatePaymentMode(PaymentMode paymentMode)
        {
            await userDbContext.PaymentModes.AddAsync(paymentMode);
            await userDbContext.SaveChangesAsync();
            return paymentMode;
        }

        public async Task<PaymentMode> UpdatePaymentMode(PaymentMode paymentMode)
        {
            userDbContext.SaveChanges();
            return paymentMode;
        }


        public async Task<bool> DeletePaymentMode(PaymentMode paymentMode)
        {
            userDbContext.PaymentModes.Remove(paymentMode);
            userDbContext.SaveChanges();
            return true;
        }
    }
}
