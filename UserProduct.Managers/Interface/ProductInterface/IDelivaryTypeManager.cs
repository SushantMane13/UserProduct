using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Managers.DTO.ProductDTO;

namespace UserProduct.Managers.Interface.ProductInterface
{
    public interface IDelivaryTypeManager
    {
        Task<List<DelivaryTypeDTO>> GetAllDelivaryTypes();

        Task<DelivaryTypeDTO> GetDelivaryTypeById(int id);

        Task<DelivaryTypeDTO> CreateDelivaryType(DelivaryTypeDTO delivaryTypeDTO);

        Task<DelivaryTypeDTO> UpdateDelivaryType(int id, DelivaryTypeDTO delivaryTypeDTO);

        Task<bool> DeleteDelivaryType(int id);
    }
}
