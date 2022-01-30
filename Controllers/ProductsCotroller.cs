using Microsoft.AspNetCore.Mvc;
using Proiect.Core.IConfig;
using Proiect.Models;
using BCryptNet = BCrypt.Net.BCrypt;
using Proiect.Models.DTOs;
using Proiect.Services;
using Proiect.Utilities.Attributes;
namespace Proiect.Controllers

{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IUnitofWork _unitOfWork;

          private IProductsService _productsService;

        public ProductsController(ILogger<ProductsController> logger, IUnitofWork unitOfWork, IProductsService productsService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _productsService = productsService;
        }

         [Authorization(Role.Admin)]
        [HttpPost]
        public async Task<IActionResult>CreateProduct(ProductsRequestDTO produs)
        {
            var productToCreate = new Products
            {
                Name = produs.Name,
                Description = produs.Description,
                Price = produs.Price
            };

            if(ModelState.IsValid)
            {
                await _unitOfWork.Produse.Add(productToCreate);
                await _unitOfWork.CompleteAsync();
                return Ok();
            }

            return new JsonResult("Something is Wrong") {StatusCode= 500};
        }

            [Authorization(Role.Admin)]
             [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(Guid id)
        {   
                var product = await _unitOfWork.Produse.GetById(id);
                if(product == null){
                    return NotFound();
                }
               return Ok(product);
        }
            [Authorization(Role.Admin)]
            [HttpGet]
        public async Task<IActionResult> Get()
        {
                var products = await _unitOfWork.Produse.All();
               return Ok(products);
        }

            [Authorization(Role.Admin)]
            [HttpPost("{id}")]
        public async Task<IActionResult> UpdateItem(Guid id, ProductsRequestDTO produs)
        {    
              if(id != produs.Id)
                    return BadRequest();
                       var productToUpdate = new Products
            {
                Id = produs.Id,
                Name = produs.Name,
                Description = produs.Description,
                Price = produs.Price
            };
            await _unitOfWork.Produse.Upsert(productToUpdate);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        } 

            [Authorization(Role.Admin)]
            [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var item = await _unitOfWork.Produse.GetById(id);
            if(item == null)
            return BadRequest();

            await _unitOfWork.Produse.Delete(id);
            await _unitOfWork.CompleteAsync();
            return Ok(item);
        } 
    }
}