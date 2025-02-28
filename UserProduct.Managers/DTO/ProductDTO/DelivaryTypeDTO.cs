

using UserProduct.Services.Models.Entity;

namespace UserProduct.Managers.DTO.ProductDTO
{
    public class DelivaryTypeDTO
    {
        public int DeliveryTypeId { get; set; }
        public string Type { get; set; } = null!;

        public static DelivaryType MapToDelivaryType(DelivaryTypeDTO delivaryTypeDTO)
        {
            return new DelivaryType
            {
                Type = delivaryTypeDTO.Type
            };
        }

        public static DelivaryTypeDTO MapToDelivaryTypeDTO(DelivaryType delivaryType)
        {
            return new DelivaryTypeDTO
            {
                DeliveryTypeId=delivaryType.DelivaryTypeId,
                Type = delivaryType.Type
            };
        }
    }
}
