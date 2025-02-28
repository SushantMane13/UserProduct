using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProduct.Managers.DTO.ProductDTO;
using UserProduct.Managers.DTO.UserDTO;
using UserProduct.Managers.Interface.ProductInterface;
using UserProduct.Services.Interface.ProductInterface;
using UserProduct.Services.Models.Entity;
using UserProduct.Managers.Exceptions;

namespace UserProduct.Managers.Implmentattion.ProductClasses
{
    public class ProductManager : IProductManager
    {
        private readonly IProductService productService;
        private readonly IProductTypeService productTypeService;
        
        public ProductManager(IProductService productService, IProductTypeService productTypeService)
        {
            this.productService = productService;
            this.productTypeService = productTypeService;
        }

        public async Task<List<ProductDTO>> GetAllProducts()
        {
            var res = await productService.GetAllProducts();
            return res.Select(x => ProductDTO.MapToProductDTO(x)).ToList();
        }

        public async Task<ProductDTO> GetProductById(int id)
        {
            List<string> exception = [];
            if (id <= 0)
                exception.Add("Enter Correct Details");

            var res = await productService.GetProductById(id);
            if(res==null)
                exception.Add("Product Does Not Exist");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            return ProductDTO.MapToProductDTO(res);
        }

        public async Task<ProductDTO> CreateProduct(ProductDTO productDTO)
        {
            List<string> exception = [];
            if ( string.IsNullOrEmpty(productDTO.Name.Trim()) || productDTO.Price <= 0 || productDTO.Quantity < 0 || productDTO.SellQuantity!=0)
                exception.Add("Enter Correct Details");

            var productType = await productTypeService.GetProductTypeById(productDTO.ProductTypeId);
            if (productType == null)
                exception.Add("Product Type Is Not Exist");

            if (!productType.IsActive)
                exception.Add("Product Type Status Is Not Active");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            var product = ProductDTO.MapToProduct(productDTO);
            var res = await productService.CreateProduct(product);
            return ProductDTO.MapToProductDTO(res);
        }

        public async Task<ProductDTO> UpdateProduct(int id, ProductDTO productDTO)
        {
            List<string> exception = [];
            if ( string.IsNullOrEmpty(productDTO.Name.Trim()) || productDTO.Price <= 0 || productDTO.Quantity < 0 || productDTO.SellQuantity < 0)
                exception.Add("Enter Correct Details");

            var productType = await productTypeService.GetProductTypeById(productDTO.ProductTypeId);
            if (productType == null)
                exception.Add("Product Type Is Not Exist");

            if (!productType.IsActive)
                exception.Add("Product Type Status Is Not Active");

            var product = await productService.GetProductById(id);
            if (product == null)
                exception.Add("Product is Not Exist");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            product.Name=productDTO.Name;
            product.ProductTypeId=productDTO.ProductTypeId;
            product.Price=productDTO.Price;
            product.Quantity=productDTO.Quantity;


            var res = await productService.UpdateProduct(product);

            return ProductDTO.MapToProductDTO(res);
        }

        public async Task<bool> DeleteProduct(int id)
        {
            List<string> exception = [];
            if (id <= 0)
                exception.Add("Enter Valid Details");

            var product = await productService.GetProductById(id);
            
            if(product==null)
                exception.Add("Product is Not Exist");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            bool res = await productService.DeleteProduct(product);
            return res;
        }
    }
}
