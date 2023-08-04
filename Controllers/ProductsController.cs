using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Entites;
using ProductApi.Exceptions;
using ProductApi.Exeptions;
using ProductApi.Managers;
using ProductApi.Models;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductManager _manager;

        public ProductsController(IProductManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery]ProductFilter filter)
        {
            var products=await _manager.GetProductsAsync(filter);
            return Ok(products);
        }

        [HttpPost]
      
        public async Task<IActionResult> CreateProduct([FromBody]CreateProductModel model)
        {
            try
            {
                return Ok(await _manager.AddProductAsync(model));
            }
            catch (IsNotValidException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{productId:guid}")]
        public async Task<IActionResult> GetProductById(Guid productId)
        {
            try
            {
                return Ok(await _manager.GetProductByIdAsync(productId));
            }
            catch (ProductNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{name:alpha}")]
        public async Task<IActionResult> GetProductByName(string name)
        {
            try
            {
                return Ok(await _manager.GetProductByNameAsync(name));
            }
            catch (ProductNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(Guid productId,CreateProductModel model)
        {
            try
            {
                return Ok(await _manager.UpdateProductAsync(productId, model));
            }
            catch (ProductNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DelateProduct(Guid productId)
        {
            try
            {
                await _manager.DeleteProductAsync(productId);
                return Ok();
            }
            catch (ProductNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
