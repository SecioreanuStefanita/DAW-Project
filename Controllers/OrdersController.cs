using Microsoft.AspNetCore.Mvc;
using Proiect.Core.IConfig;
using Proiect.Models;
using BCryptNet = BCrypt.Net.BCrypt;
using Proiect.Models.DTOs;
using Proiect.Services;
using Proiect.Utilities.Attributes;
using System.Text.RegularExpressions;
namespace Proiect.Controllers

{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IUnitofWork _unitOfWork;

          private IOrdersService _Orderservice;

        public OrdersController(ILogger<OrdersController> logger, IUnitofWork unitOfWork, IOrdersService Orderservice)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _Orderservice = Orderservice;
        }



       

        [HttpPost]
        public async Task<IActionResult>CreateOrders(OrdersCreateDTO Orders)
        {

             var Product = await _unitOfWork.Produse.GetById(Orders.ProductsId);
            var priceOrderString = Product.Price.Split("/")[0];
            string result = Regex.Replace(priceOrderString, @"[^\d]", "");

            var OrdersToCreate = new Orders
            {
              UserId = Orders.UserId,
              Price = result,
              ProductsId = Orders.ProductsId
            };

            if(ModelState.IsValid)
            {
                await _unitOfWork.Orders.Add(OrdersToCreate);
                await _unitOfWork.CompleteAsync();
                return Ok();
            }

            return new JsonResult("Something is Wrong") {StatusCode= 500};
        }

            [Authorization(Role.Admin)]
             [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(Guid id)
        {         Console.WriteLine(id);
                var Orders = await _unitOfWork.Orders.GetById(id);
                if(Orders == null){
                    return NotFound();
                }
               return Ok(Orders);
        }
            [Authorization(Role.Admin)]
            [HttpGet]
        public async Task<IActionResult> Get()
        {
                var Orders = await _unitOfWork.Orders.All();
               return Ok(Orders);
        }

            [Authorization(Role.Admin)]
            [HttpPost("{id}")]
        public async Task<IActionResult> UpdateItem(Guid id, OrdersRequestDTO Orders)
        {    
              if(id != Orders.Id)
                    return BadRequest();

                      var OrdersToUpdate = new Orders
            {
                Id = Orders.Id,
                UserId = Orders.UserId,
                ProductsId = Orders.ProductsId,
                Price = Orders.Price
            };
            await _unitOfWork.Orders.Upsert(OrdersToUpdate);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        } 

            [Authorization(Role.Admin)]
            [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var item = await _unitOfWork.Orders.GetById(id);
            if(item == null)
            return BadRequest();

            await _unitOfWork.Orders.Delete(id);
            await _unitOfWork.CompleteAsync();
            return Ok(item);
        } 
    }
}