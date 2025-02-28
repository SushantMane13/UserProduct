using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Managers.DTO.ProductDTO;
using UserProduct.Managers.Exceptions;
using UserProduct.Managers.Interface.ProductInterface;
using UserProduct.Services.Interface.ProductInterface;

namespace UserProduct.Managers.Implmentattion.ProductClasses
{
    public class ProductTypeManager : IProductTypeManager
    {
        private readonly IProductTypeService productTypeService;
        
        public ProductTypeManager(IProductTypeService productTypeService)
        {
            this.productTypeService = productTypeService;
        }

        public async Task<List<ProductTypeDTO>> GetAllProductTypes()
        {
            var res = await productTypeService.GetAllProductTypes();
            return res.Select(x => ProductTypeDTO.MapToProductTypeDTO(x)).ToList();
        }

        public async Task<ProductTypeDTO> GetProductTypeById(int id)
        {
            List<string> exception = [];
            if (id <= 0)
                exception.Add("Enter Valid Details");

            var res = await productTypeService.GetProductTypeById(id);
            if (res == null)
                exception.Add("ProductType does not Exist");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            return ProductTypeDTO.MapToProductTypeDTO(res);
        }

        public async Task<ProductTypeDTO> CreateProductType(ProductTypeDTO productTypeDTO)
        {
            List<string> exception = [];
            if ( productTypeDTO.TypeName.Trim().Length == 0)
                exception.Add("Enter Valid Details");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            var productType = ProductTypeDTO.MapToProductType(productTypeDTO);
            var res = await productTypeService.CreateProductType(productType);
            return ProductTypeDTO.MapToProductTypeDTO(res);
        }

        public async Task<ProductTypeDTO> UpdateProductType(int id, ProductTypeDTO productTypeDTO)
        {
            List<string> exception = [];

            if (string.IsNullOrEmpty(productTypeDTO.TypeName.Trim()) || id<=0)
                exception.Add("Enter Valid Details");

            var productType = await productTypeService.GetProductTypeById(id);
            if (productType == null)
                exception.Add("ProductType does not Exist");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            productType.TypeName = productTypeDTO.TypeName;
            productType.IsActive = productTypeDTO.IsActive;

            var res = await productTypeService.UpdateProductType(productType);

            return ProductTypeDTO.MapToProductTypeDTO(res);
        }

        public async Task<bool> DeleteProductType(int id)
        {
            List<string> exception = [];
            if (id<=0)
                exception.Add("Enter Valid Details");

            var productType = await productTypeService.GetProductTypeById(id);
            if (productType == null)
                exception.Add("ProductType does not Exist");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            bool res = await productTypeService.DeleteProductType(productType);
            return res;
        }
    }
}
