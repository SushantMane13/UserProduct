using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Managers.DTO.ProductDTO;

namespace UserProduct.Managers.Interface.ProductInterface
{
    public interface IProductTypeManager
    {
        Task<List<ProductTypeDTO>> GetAllProductTypes();

        Task<ProductTypeDTO> GetProductTypeById(int id);

        Task<ProductTypeDTO> CreateProductType(ProductTypeDTO productTypeDTO);

        Task<ProductTypeDTO> UpdateProductType(int id, ProductTypeDTO productTypeDTO);

        Task<bool> DeleteProductType(int id);
    }
}
