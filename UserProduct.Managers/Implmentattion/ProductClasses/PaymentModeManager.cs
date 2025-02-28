using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Managers.DTO.ProductDTO;
using UserProduct.Managers.Exceptions;
using UserProduct.Managers.Interface.ProductInterface;
using UserProduct.Services.Interface.ProductInterface;
using UserProduct.Managers.Exceptions;

namespace UserProduct.Managers.Implmentattion.ProductClasses
{
    public class PaymentModeManager : IPaymentModeManager
    {
        private readonly IPaymentModeService paymentModeService;
        
        public PaymentModeManager(IPaymentModeService paymentModeService)
        {
            this.paymentModeService = paymentModeService;
        }

        public async Task<List<PaymentModeDTO>> GetAllPaymentModes()
        {
            var res = await paymentModeService.GetAllPaymentModes();
            return res.Select(x => PaymentModeDTO.MapToPaymentModeDTO(x)).ToList();
        }

        public async Task<PaymentModeDTO> GetPaymentModeById(int id)
        {
            List<string> exception = [];
            if (id <= 0)
                exception.Add("Enter Valid Details");

            var res = await paymentModeService.GetPaymentModeById(id);
            if (res == null)
                exception.Add("PaymentMode Does Not Exist");
                
            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            return PaymentModeDTO.MapToPaymentModeDTO(res);
        }

        public async Task<PaymentModeDTO> CreatePaymentMode(PaymentModeDTO paymentModeDTO)
        {
            List<string> exception = [];
            if (string.IsNullOrEmpty(paymentModeDTO.Mode.Trim()) || string.IsNullOrEmpty(paymentModeDTO.SubMode.Trim()))
                exception.Add("Enter Valid Details");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            var paymentMode = PaymentModeDTO.MapToPaymentMode(paymentModeDTO);
            var res = await paymentModeService.CreatePaymentMode(paymentMode);
            return PaymentModeDTO.MapToPaymentModeDTO(res);
        }

        public async Task<PaymentModeDTO> UpdatePaymentMode(int id, PaymentModeDTO paymentModeDTO)
        {
            List<string> exception = [];
            if (string.IsNullOrEmpty(paymentModeDTO.Mode.Trim()) || string.IsNullOrEmpty(paymentModeDTO.SubMode.Trim()) || id<=0)
                exception.Add("Enter Valid Details");


            var paymentMode = await paymentModeService.GetPaymentModeById(id);
            if (paymentMode == null)
                exception.Add("PaymentMode Does Not Exist");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            paymentMode.Mode = paymentModeDTO.Mode;
            paymentMode.SubMode= paymentModeDTO.SubMode;

            var res = await paymentModeService.UpdatePaymentMode(paymentMode);

            return PaymentModeDTO.MapToPaymentModeDTO(res);
        }

        public async Task<bool> DeletePaymentMode(int id)
        {
            List<string> exception = [];
            if (id <= 0)
                exception.Add("Enter Valid Details");

            var paymentMode = await paymentModeService.GetPaymentModeById(id);
            if (paymentMode == null)
                exception.Add("PaymentMode Does Not Exist");
            
            if(exception.Count!=0)
                throw new ValidationException(String.Join(",\n", exception));

            bool res = await paymentModeService.DeletePaymentMode(paymentMode);
            return res;
        }
    }
}
