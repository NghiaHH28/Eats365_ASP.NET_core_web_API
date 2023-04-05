using DataAccess.Repository;
using EATS365_Library.Paging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using EATS365_Library.DTO;

namespace EATS365_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductRepository _productRepository;

        public ProductController() => _productRepository = new ProductRepository();

        [HttpGet]
        public IActionResult GetProducts(string searchTitle, string searchOrder, string searchName, string searchCategory, int? pageNumber = 1)
        {
            try
            {
                var products = _productRepository.GetAllProducts();

                if (!(searchCategory is null))
                {
                    products = products.Where(p => p.CategoryId.Equals(searchCategory)).ToList();
                }

                if (!String.IsNullOrEmpty(searchName))
                {
                    products = products.Where(p => p.ProductName.ToLower().Contains(searchName.ToLower())).ToList();
                }

                if (!String.IsNullOrEmpty(searchOrder))
                {
                    if (searchOrder.Contains("desc"))
                    {
                        switch (searchTitle)
                        {
                            case "name":
                                products = products.OrderByDescending(p => p.ProductName).ToList();
                                break;
                            case "price":
                                products = products.OrderByDescending(p => p.ProductPrice).ToList();
                                break;
                        }
                    }
                    else
                    {
                        switch (searchTitle)
                        {
                            case "name":
                                products = products.OrderBy(p => p.ProductName).ToList();
                                break;
                            case "price":
                                products = products.OrderBy(p => p.ProductPrice).ToList();
                                break;
                        }
                    }
                }

                int pageIndex = pageNumber.Value;
                int pageSize = 10;
                int totalPage = (int)Math.Ceiling(products.Count() / (double)pageSize);

                PaginatedList<ProductDTO> listProducts = PaginatedList<ProductDTO>.Create(products.AsQueryable(), pageNumber ?? 1, pageSize);

                return Ok(new PagingReponse
                {
                    Success = true,
                    List = listProducts,
                    TotalPages = listProducts.TotalPages,
                    PageIndex = listProducts.PageIndex
                });
            }
            catch
            {
                return Ok(new PagingReponse { 
                    Success = false, 
                    Message = "Không có sản phẩm nào được tìm thấy!"
                });
            }
        }
    }
}
