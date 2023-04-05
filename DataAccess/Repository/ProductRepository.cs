using DataAccess.DAO;
using EATS365_Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public IEnumerable<ProductDTO> GetAllProducts() => ProductDAO.Instance.GetProducts();
    }
}
