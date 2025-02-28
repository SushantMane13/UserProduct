using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Services.Models.Entity;

namespace UserProduct.Services.Interface.ProductInterface
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();

        Task<Product> GetProductById(int id);

        Task<Product> CreateProduct(Product product);

        Task<Product> UpdateProduct(Product product);

        Task<bool> DeleteProduct(Product product);
    }
}
