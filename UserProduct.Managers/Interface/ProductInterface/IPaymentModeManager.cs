using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Managers.DTO.ProductDTO;

namespace UserProduct.Managers.Interface.ProductInterface
{
    public interface IPaymentModeManager
    {
        Task<List<PaymentModeDTO>> GetAllPaymentModes();

        Task<PaymentModeDTO> GetPaymentModeById(int id);

        Task<PaymentModeDTO> CreatePaymentMode(PaymentModeDTO paymentModeDTO);

        Task<PaymentModeDTO> UpdatePaymentMode(int id, PaymentModeDTO paymentModeDTO);

        Task<bool> DeletePaymentMode(int id);
    }
}
