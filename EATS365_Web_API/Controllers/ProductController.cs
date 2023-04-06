using DataAccess.Repository;
using EATS365_Library.Paging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using EATS365_Library.DTO;
using Microsoft.AspNetCore.Authorization;

namespace EATS365_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductRepository _productRepository;

        public ProductController() => _productRepository = new ProductRepository();

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult Get(string searchTitle, string searchOrder, string searchName, string searchCategory, int? pageNumber = 1)
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
            catch (Exception ex) 
            {
                return Ok(new PagingReponse
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult Get(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Ok(new APIResponseDTO
                {
                    Success = false,
                    Message = "ID is null!"
                });
            }

            try
            {
                var product = _productRepository.GetProductByProductID(id);

                return Ok(new APIResponseDTO
                {
                    Success = true,
                    Data = product
                });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponseDTO
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Post([FromBody] ProductDTO product)
        {
            if (product == null)
            {
                return Ok(new APIResponseDTO
                {
                    Success = false,
                    Message = "Product is null!"
                });
            }

            try
            {
                product.ProductId = _productRepository.GetNewProductID(product.ProductName);
                _productRepository.AddProduct(product);

                return Ok(new APIResponseDTO
                {
                    Success = true,
                    Message = "Add successful!"
                });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponseDTO
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult Put(string id, [FromBody] ProductDTO product)
        {
            if (product == null)
            {
                return Ok(new APIResponseDTO
                {
                    Success = false,
                    Message = "Product is null!"
                });
            }

            if (string.IsNullOrEmpty(id))
            {
                return Ok(new APIResponseDTO
                {
                    Success = false,
                    Message = "ID is null!"
                });
            }

            if (!id.Equals(product.ProductId))
            {
                return Ok(new APIResponseDTO
                {
                    Success = false,
                    Message = "ID and ID of product is not equal!"
                });
            }

            try
            {
                _productRepository.UpdateProduct(product);

                return Ok(new APIResponseDTO
                {
                    Success = true,
                    Message = "Update successful!"
                });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponseDTO
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Ok(new APIResponseDTO
                {
                    Success = false,
                    Message = "ID is null!"
                });
            }

            try
            {
                _productRepository.DeleteProduct(id);

                return Ok(new APIResponseDTO
                {
                    Success = true,
                    Message = "Delete successful!"
                });
            }
            catch (Exception ex)
            {
                return Ok(new APIResponseDTO
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }
    }
}
