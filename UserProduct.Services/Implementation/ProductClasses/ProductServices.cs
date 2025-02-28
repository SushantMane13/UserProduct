using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserProduct.Services.Interface.ProductInterface;
using UserProduct.Services.Models.Entity;
using UserProduct.Services.Models;

namespace UserProduct.Services.Implementation.ProductClasses
{
    public class ProductServices : IProductService
    {
        private readonly UserDbContext userDbContext;

        public ProductServices(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await userDbContext.Products.Include(a=>a.ProductType).ToListAsync();
        }


        public async Task<Product> GetProductById(int id)
        {
            return await userDbContext.Products.FindAsync(id);
        }

        public async Task<Product> CreateProduct(Product product)
        {
            await userDbContext.Products.AddAsync(product);
            await userDbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            userDbContext.SaveChanges();
            return product;
        }

        public async Task<bool> DeleteProduct(Product product)
        {
            userDbContext.Products.Remove(product);
            userDbContext.SaveChanges();
            return true;
        }
                
    }
}
