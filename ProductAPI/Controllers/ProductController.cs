using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Data.Context;
using Web.Data.Models;
using Web.Services.ProductDTO;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;


         

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm]ProductCreationDTO productDto)
        {
            if (productDto is not null)
            {
                var product = mapper.Map<Product>(productDto);
                await context.Products.AddAsync(product);
                await context.SaveChangesAsync();
                return Ok(product);
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await context.Products.ToListAsync();
            return Ok(products);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product is null)
                return NotFound("Product is not found");
            return Ok(product);
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductCreationDTO product)
        {
            if (product is not null)
            {
                var uproduct = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (uproduct is null)
                    return NotFound("Product is not found");
                mapper.Map(product, uproduct);
                context.Products.Attach(uproduct);
                context.Entry(uproduct).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(uproduct);
            }
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct([FromForm] int id)
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product is null)
                return NotFound("Product dis noit found");
            context.Products.Remove(product);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
