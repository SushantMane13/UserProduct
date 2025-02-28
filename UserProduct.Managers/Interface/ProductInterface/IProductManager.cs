using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Managers.DTO.ProductDTO;

namespace UserProduct.Managers.Interface.ProductInterface
{
    public interface IProductManager
    {
        Task<List<ProductDTO>> GetAllProducts();

        Task<ProductDTO> GetProductById(int id);

        Task<ProductDTO> CreateProduct(ProductDTO productDTO);

        Task<ProductDTO> UpdateProduct(int id, ProductDTO productDTO);

        Task<bool> DeleteProduct(int id);
    }
}
