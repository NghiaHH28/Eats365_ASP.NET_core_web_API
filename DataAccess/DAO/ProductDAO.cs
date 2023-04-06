using DataAccess.Context;
using DataAccess.Hash;
using EATS365_Library.DTO;
using EATS365_Library.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
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
                throw new Exception("Not found!");
            }
        }

        public ProductDTO GetProductDTOByProductID(string productId)
        {
            if (productId == null) return null;

            string productStatus = "REMOVED";
            Product product = null;
            try
            {
                product = _context.Products.Include(p => p.Reviews).FirstOrDefault(p => !p.ProductStatus.Equals(productStatus) && p.ProductId == productId);
            }
            catch (Exception ex)
            {
                throw new Exception("Not found!");
            }

            List<ReviewDTO> reviews = product.Reviews.Select(r => new ReviewDTO
            {
                ReviewId = r.ReviewId,
                Rating = r.Rating,
                Review1 = r.Review1,
                ReviewStatus = r.ReviewStatus,
                ReviewDay = r.ReviewDay,
                ReviewRemoveDay = r.ReviewRemoveDay,
                ProductId = r.ProductId,
                AccountId = r.AccountId,
                ReplyId = r.ReplyId
            }).ToList();

            ProductDTO productDTO = new ProductDTO
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                ProductStatus = product.ProductStatus,
                ProductImage = product.ProductImage,
                ProductPrice = product.ProductPrice,
                ProductSalePercent = product.ProductSalePercent,
                CategoryId = product.CategoryId,
                Reviews = reviews
            };

            return productDTO;
        }

        public Product GetProductByProductID(string productId)
        {
            if (productId == null) return null;

            Product product = null;
            try
            {
                product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
            }
            catch (Exception ex)
            {
                throw new Exception("Not found!");
            }

            return product;
        }

        public void AddProduct(ProductDTO productDTO)
        {
            if (productDTO == null) return;

            try
            {
                Product _product = GetProductByProductID(productDTO.ProductId);
                if (_product == null)
                {
                    Product product = new Product
                    {
                        ProductName = productDTO.ProductName,
                        ProductDescription = productDTO.ProductDescription,
                        ProductStatus = productDTO.ProductStatus,
                        ProductImage = productDTO.ProductImage,
                        ProductPrice = productDTO.ProductPrice,
                        ProductId = productDTO.ProductId,
                        ProductSalePercent = productDTO.ProductSalePercent,
                        CategoryId = productDTO.CategoryId
                    };

                    _context.Products.Add(product);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("The productID is already exist!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateProduct(ProductDTO productDTO)
        {
            if (productDTO == null) return;

            try
            {
                Product _product = GetProductByProductID(productDTO.ProductId);
                if (_product != null)
                {
                    _context.Entry(_product).CurrentValues.SetValues(productDTO);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("The productID is not already exist!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteProduct(string productID)
        {
            if (productID == null) return;

            try
            {
                Product _product = GetProductByProductID(productID);
                if (_product != null)
                {
                    _product.ProductStatus = "REMOVED";
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("The productID is not already exist!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();
            var strConn = config["ConnectionString:Eats365DB"];
            return strConn;
        }
        public string GetLastProductID()
        {
            var connectionString = GetConnectionString();
            var sqlQuery = @"SELECT TOP 1 ProductID
                     FROM Product
                     ORDER BY CAST(RIGHT(ProductId, 4) AS INT) DESC";

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sqlQuery, connection))
            {
                connection.Open();
                var result = command.ExecuteScalar();
                return result?.ToString();
            }
        }

        public string GetNewProductID(string productName)
        {
            GenerageID g = new GenerageID();
            string productID = g.GenerateNewID(g.GetPrefixFromProductName(productName), GetLastProductID());

            return productID;
        }
    }
}
