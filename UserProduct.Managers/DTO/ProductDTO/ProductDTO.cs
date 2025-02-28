using UserProduct.Services.Models.Entity;

namespace UserProduct.Managers.DTO.ProductDTO
{
    public class ProductDTO
    {
        public int ProductId {  get; set; }
        public string Name { get; set; } = null!;

        public int ProductTypeId { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }

        public int SellQuantity { get; set; }

        public ProductTypeDTO ProductType { get; set; }

        public static Product MapToProduct(ProductDTO productDTO)
        {
            return new Product
            {
                Name = productDTO.Name,
                ProductTypeId = productDTO.ProductTypeId,
                Price = productDTO.Price,
                Quantity = productDTO.Quantity,
                SellQuantity = productDTO.SellQuantity
            };
        }

        public static ProductDTO MapToProductDTO(Product product)
        {
            return new ProductDTO
            {
                ProductId = product.ProductId,
                Name = product.Name,
                ProductTypeId = product.ProductTypeId,
                Price = product.Price,
                Quantity = product.Quantity,
                SellQuantity = product.SellQuantity,
                ProductType=ProductTypeDTO.MapToProductTypeDTO(product.ProductType)
            };
        }
    }
}
