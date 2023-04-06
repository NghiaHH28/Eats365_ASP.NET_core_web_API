using EATS365_Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IProductRepository
    {
        public IEnumerable<ProductDTO> GetAllProducts();
        public ProductDTO GetProductByProductID(string productID);
        public void AddProduct(ProductDTO product);
        public void UpdateProduct(ProductDTO product);
        public void DeleteProduct(string productID);
        public string GetNewProductID(string productName);
    }
}
