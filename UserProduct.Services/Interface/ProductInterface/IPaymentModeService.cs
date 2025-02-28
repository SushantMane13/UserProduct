using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Services.Models.Entity;

namespace UserProduct.Services.Interface.ProductInterface
{
    public interface IPaymentModeService
    {
        Task<List<PaymentMode>> GetAllPaymentModes();

        Task<PaymentMode> GetPaymentModeById(int id);

        Task<PaymentMode> CreatePaymentMode(PaymentMode paymentMode);

        Task<PaymentMode> UpdatePaymentMode(PaymentMode paymentMode);

        Task<bool> DeletePaymentMode(PaymentMode paymentMode);
    }
}
