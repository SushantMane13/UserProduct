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
    public class ProductTypeService : IProductTypeService
    {
        private readonly UserDbContext userDbContext;

        public ProductTypeService(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }

        public async Task<List<ProductType>> GetAllProductTypes()
        {
            return await userDbContext.ProductTypes.ToListAsync();
        }


        public async Task<ProductType> GetProductTypeById(int id)
        {
            return await userDbContext.ProductTypes.FindAsync(id);
        }

        public async Task<ProductType> CreateProductType(ProductType productType)
        {
            await userDbContext.ProductTypes.AddAsync(productType);
            await userDbContext.SaveChangesAsync();
            return productType;
        }

        public async Task<ProductType> UpdateProductType(ProductType productType)
        {
            userDbContext.SaveChanges();
            return productType;
        }

        public async Task<bool> DeleteProductType(ProductType productType)
        {
            userDbContext.ProductTypes.Remove(productType);
            userDbContext.SaveChanges();
            return true;
        }
    }
}
