using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Services.Models.Entity;

namespace UserProduct.Services.Interface.ProductInterface
{
    public interface IDelivaryTypeServices
    {
        Task<List<DelivaryType>> GetAllDelivaryTypes();

        Task<DelivaryType> GetDelivaryTypeById(int id);

        Task<DelivaryType> CreateDelivaryType(DelivaryType delivaryType);

        Task<DelivaryType> UpdateDelivaryType(DelivaryType delivaryType);

        Task<bool> DeleteDelivaryType(DelivaryType delivaryType);

    }
}
