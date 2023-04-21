using DataAccess.Repository;
using EATS365_Library.Paging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using EATS365_Library.DTO;
using Microsoft.AspNetCore.Authorization;
using EATS365_Library.EATS365_Exception;

namespace EATS365_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductRepository _productRepository;

        public ProductController() => _productRepository = new ProductRepository();

        [HttpGet]
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
                return BadRequest(new PagingReponse
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(new APIResponseDTO
                {
                    Success = false,
                    UserMessage = "ID of product can not be empty!",
                    StatusCode = 400,
                    Data = null
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
            catch (ProductNotFoundException ex)
            {
                return NotFound(new APIResponseDTO
                {
                    Success = false,
                    UserMessage = ex.Message,
                    InternalMessage = ex.ToString(),
                    StatusCode = 404,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new APIResponseDTO
                {
                    Success = false,
                    UserMessage = "An error occurred during processing. Please try again later!",
                    InternalMessage = ex.ToString(),
                    StatusCode = 500,
                    Data = null
                });
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Post([FromBody] ProductDTO product)
        {
            if (product == null)
            {
                return BadRequest(new APIResponseDTO
                {
                    Success = false,
                    UserMessage = "Product can not be empty!",
                    StatusCode = 400,
                    Data = null
                });
            }

            try
            {
                product.ProductId = _productRepository.GetNewProductID(product.ProductName);
                _productRepository.AddProduct(product);

                return Ok(new APIResponseDTO
                {
                    Success = true,
                    UserMessage = "Add successful!"
                });
            }
            catch (ProductAlreadyExistException ex)
            {
                return Conflict(new APIResponseDTO
                {
                    Success = false,
                    UserMessage = ex.Message,
                    InternalMessage = ex.ToString(),
                    StatusCode = 409,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new APIResponseDTO
                {
                    Success = false,
                    UserMessage = "An error occurred during processing. Please try again later!",
                    InternalMessage = ex.ToString(),
                    StatusCode = 500,
                    Data = null
                });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult Put(string id, [FromBody] ProductDTO product)
        {
            if (product == null)
            {
                return BadRequest(new APIResponseDTO
                {
                    Success = false,
                    UserMessage = "Product can not be empty!",
                    StatusCode = 400,
                    Data = null
                });
            }

            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(new APIResponseDTO
                {
                    Success = false,
                    UserMessage = "ID of product can not be empty!",
                    StatusCode = 400,
                    Data = null
                });
            }

            if (!id.Equals(product.ProductId))
            {
                return Conflict(new APIResponseDTO
                {
                    Success = false,
                    UserMessage = "ID and ID of product is not equal!",
                    StatusCode = 409,
                    Data = null
                });
            }

            try
            {
                _productRepository.UpdateProduct(product);

                return Ok(new APIResponseDTO
                {
                    Success = true,
                    UserMessage = "Update successful!"
                });
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound(new APIResponseDTO
                {
                    Success = false,
                    UserMessage = ex.Message,
                    InternalMessage = ex.ToString(),
                    StatusCode = 404,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new APIResponseDTO
                {
                    Success = false,
                    UserMessage = "An error occurred during processing. Please try again later!",
                    InternalMessage = ex.ToString(),
                    StatusCode = 500,
                    Data = null
                });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(new APIResponseDTO
                {
                    Success = false,
                    UserMessage = "ID of product can not be empty!",
                    StatusCode = 400,
                    Data = null
                });
            }

            try
            {
                _productRepository.DeleteProduct(id);

                return Ok(new APIResponseDTO
                {
                    Success = true,
                    UserMessage = "Delete successful!"
                });
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound(new APIResponseDTO
                {
                    Success = false,
                    UserMessage = ex.Message,
                    InternalMessage = ex.ToString(),
                    StatusCode = 404,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new APIResponseDTO
                {
                    Success = false,
                    UserMessage = "An error occurred during processing. Please try again later!",
                    InternalMessage = ex.ToString(),
                    StatusCode = 500,
                    Data = null
                });
            }
        }
    }
}
