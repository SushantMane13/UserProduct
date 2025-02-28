

using UserProduct.Services.Models.Entity;

namespace UserProduct.Managers.DTO.ProductDTO
{
    public class PaymentModeDTO
    {
        public int PaymentModeId { get; set; }
        public string Mode { get; set; } = null!;

        public string SubMode { get; set; } = null!;

        public static PaymentMode MapToPaymentMode(PaymentModeDTO paymentModeDTO)
        {
            return new PaymentMode
            {
                Mode = paymentModeDTO.Mode,
                SubMode = paymentModeDTO.SubMode
            };
        }

        public static PaymentModeDTO MapToPaymentModeDTO(PaymentMode paymentMode)
        {
            return new PaymentModeDTO
            {
                PaymentModeId=paymentMode.PaymentModeId,
                Mode = paymentMode.Mode,
                SubMode = paymentMode.SubMode
            };
        }
    }
}
