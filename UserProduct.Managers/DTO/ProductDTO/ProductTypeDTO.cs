using UserProduct.Services.Models.Entity;


namespace UserProduct.Managers.DTO.ProductDTO
{
    public class ProductTypeDTO
    {
        public string TypeName { get; set; } = null!;

        public bool IsActive { get; set; }

        public static ProductType MapToProductType(ProductTypeDTO productTypeDTO)
        {
            return new ProductType
            {
                TypeName = productTypeDTO.TypeName,
                IsActive = productTypeDTO.IsActive
            };
        }

        public static ProductTypeDTO MapToProductTypeDTO(ProductType productType)
        {
            return new ProductTypeDTO
            {
                TypeName = productType.TypeName,
                IsActive = productType.IsActive
            };
        }
    }
}
