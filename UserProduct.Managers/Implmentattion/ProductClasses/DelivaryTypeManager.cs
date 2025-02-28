using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Managers.DTO.ProductDTO;
using UserProduct.Managers.Interface.ProductInterface;
using UserProduct.Services.Implementation.ProductClasses;
using UserProduct.Services.Interface.ProductInterface;
using UserProduct.Managers.Exceptions;

namespace UserProduct.Managers.Implmentattion.ProductClasses
{
    public class DelivaryTypeManager : IDelivaryTypeManager
    {
        private readonly IDelivaryTypeServices delivaryTypeServices;
        
        public DelivaryTypeManager(IDelivaryTypeServices delivaryTypeServices)
        {
            this.delivaryTypeServices = delivaryTypeServices;
        }

        public async Task<List<DelivaryTypeDTO>> GetAllDelivaryTypes()
        {
            var res = await delivaryTypeServices.GetAllDelivaryTypes();
            return res.Select(x => DelivaryTypeDTO.MapToDelivaryTypeDTO(x)).ToList();
        }

        public async Task<DelivaryTypeDTO> GetDelivaryTypeById(int id)
        {
            List<string> exception = [];
            if (id <= 0)
                exception.Add("Enter Valid Details");

            var res = await delivaryTypeServices.GetDelivaryTypeById(id);
            if (res == null)
                exception.Add("DeliveryType is not exist");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            return DelivaryTypeDTO.MapToDelivaryTypeDTO(res);
        }

        public async Task<DelivaryTypeDTO> CreateDelivaryType(DelivaryTypeDTO delivaryTypeDTO)
        {
            List<string> exception = [];
            if (string.IsNullOrEmpty(delivaryTypeDTO.Type.Trim()))
                exception.Add("Enter Valid Details");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            var delivaryType = DelivaryTypeDTO.MapToDelivaryType(delivaryTypeDTO);
            var res = await delivaryTypeServices.CreateDelivaryType(delivaryType);
            return DelivaryTypeDTO.MapToDelivaryTypeDTO(res);
        }

        public async Task<DelivaryTypeDTO> UpdateDelivaryType(int id, DelivaryTypeDTO delivaryTypeDTO)
        {
            List<string> exception = [];
            if (string.IsNullOrEmpty(delivaryTypeDTO.Type.Trim()) || id<=0)
                exception.Add("Enter Valid Details");
                      
            var delivaryType = await delivaryTypeServices.GetDelivaryTypeById(id);
            if (delivaryType == null)
                exception.Add("DeliveryType is not exist");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            delivaryType.Type = delivaryTypeDTO.Type;

            var res = await delivaryTypeServices.UpdateDelivaryType(delivaryType);

            return DelivaryTypeDTO.MapToDelivaryTypeDTO(res);
        }

        public async Task<bool> DeleteDelivaryType(int id)
        {
            List<string> exception = [];
            if (id <= 0)
                exception.Add("Enter Valid Details");

            var delivaryType = await delivaryTypeServices.GetDelivaryTypeById(id);
            if (delivaryType == null)
                exception.Add("DeliveryType is not exist");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            bool res = await delivaryTypeServices.DeleteDelivaryType(delivaryType);
            return res;
        }
    }
}
