using DataAccess.Context;
using EATS365_Library.DTO;
using EATS365_Library.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ProductDAO
    {
        private EATS365Context _context;
        private static ProductDAO _instance = null;
        private static readonly object _instanceLock = new object();

        public static ProductDAO Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new ProductDAO();
                    }
                    return _instance;
                }
            }
        }

        public ProductDAO() => _context = new EATS365Context();

        public IEnumerable<ProductDTO> GetProducts()
        {
            var products = new List<Product>();

            try
            {
                products = _context.Products.ToList();

                var productDTOs = products.Select(p => new ProductDTO
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductDescription = p.ProductDescription,
                    ProductStatus = p.ProductStatus,
                    ProductImage = p.ProductImage,
                    ProductPrice = p.ProductPrice,
                    ProductSalePercent = p.ProductSalePercent,
                    CategoryId = p.CategoryId
                });

                return productDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
