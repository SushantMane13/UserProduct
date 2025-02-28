using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Services.Models.Entity;

namespace UserProduct.Services.Interface.ProductInterface
{
    public interface IProductTypeService
    {
        Task<List<ProductType>> GetAllProductTypes();

        Task<ProductType> GetProductTypeById(int id);

        Task<ProductType> CreateProductType(ProductType productType);

        Task<ProductType> UpdateProductType(ProductType productType);

        Task<bool> DeleteProductType(ProductType productType);
    }
}
