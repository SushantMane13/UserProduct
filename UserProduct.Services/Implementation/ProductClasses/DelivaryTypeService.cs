using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserProduct.Services.Interface.ProductInterface;
using UserProduct.Services.Models;
using UserProduct.Services.Models.Entity;

namespace UserProduct.Services.Implementation.ProductClasses
{
    public class DelivaryTypeService : IDelivaryTypeServices
    {
        private readonly UserDbContext userDbContext;

        public DelivaryTypeService(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }

        public async Task<List<DelivaryType>> GetAllDelivaryTypes()
        {
            return await userDbContext.DelivaryTypes.ToListAsync();
        }


        public async Task<DelivaryType> GetDelivaryTypeById(int id)
        {
            return await userDbContext.DelivaryTypes.FindAsync(id);
        }

        public async Task<DelivaryType> CreateDelivaryType(DelivaryType delivaryType)
        {
            await userDbContext.DelivaryTypes.AddAsync(delivaryType);
            await userDbContext.SaveChangesAsync();
            return delivaryType;
        }

        public async Task<DelivaryType> UpdateDelivaryType(DelivaryType delivaryType)
        {
            userDbContext.SaveChanges();
            return delivaryType;
        }

        public async Task<bool> DeleteDelivaryType(DelivaryType delivaryType)
        {
            userDbContext.DelivaryTypes.Remove(delivaryType);
            userDbContext.SaveChanges();
            return true;
        }
    }
}
