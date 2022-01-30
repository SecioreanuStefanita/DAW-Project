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
    public class RentingsController : ControllerBase
    {
        private readonly ILogger<RentingsController> _logger;
        private readonly IUnitofWork _unitOfWork;

          private IRentingsService _Rentingservice;

        public RentingsController(ILogger<RentingsController> logger, IUnitofWork unitOfWork, IRentingsService Rentingservice)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _Rentingservice = Rentingservice;
        }



        [HttpPost]
        public async Task<IActionResult>CreateRentings(RentingsCreateDTO Rentings)
        {
            var Product = await _unitOfWork.Produse.GetById(Rentings.ProductsId);
            var priceRentString = Product.Price.Split("/")[1];
            string result = Regex.Replace(priceRentString, @"[^\d]", "");
            var numDays = DateTime.Parse(Rentings.DateFinished).DayOfYear -  DateTime.Parse(Rentings.DateStart).DayOfYear;
            var PriceCalculated = Int32.Parse(result) * numDays;
            
            var RentingsToCreate = new Rentings
            {
                UserId = Rentings.UserId,
                ProductsId = Rentings.ProductsId,
                DateStart = Rentings.DateStart,
                DateFinished = Rentings.DateFinished,
                TotalPrice = PriceCalculated.ToString(),
                 Id = Guid.NewGuid()
            };

            if(ModelState.IsValid)
            {
                await _unitOfWork.Rentings.Add(RentingsToCreate);
                await _unitOfWork.CompleteAsync();
                return Ok();
            }

            return new JsonResult("Something is Wrong") {StatusCode= 500};
        }

            [Authorization(Role.Admin)]
             [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(Guid id)
        {         Console.WriteLine(id);
                var Rentings = await _unitOfWork.Rentings.GetById(id);
                if(Rentings == null){
                    return NotFound();
                }
               return Ok(Rentings);
        }
            [Authorization(Role.Admin)]
            [HttpGet]
        public async Task<IActionResult> Get()
        {
                var Rentings = await _unitOfWork.Rentings.All();
               return Ok(Rentings);
        }

            [Authorization(Role.Admin)]
            [HttpPost("{id}")]
        public async Task<IActionResult> UpdateItem(Guid id, RentingsRequestDTO Rentings)
        {    
            var Product = await _unitOfWork.Produse.GetById(Rentings.ProductsId);
            var priceRentString = Product.Price.Split("/")[1];
            string result = Regex.Replace(priceRentString, @"[^\d]", "");
            var numDays = DateTime.Parse(Rentings.DateFinished).DayOfYear -  DateTime.Parse(Rentings.DateStart).DayOfYear;
            var PriceCalculated = Int32.Parse(result) * numDays;

              if(id != Rentings.Id)
                    return BadRequest();
            var RentingsToUpdate = new Rentings
            {
                Id = Rentings.Id,
                UserId = Rentings.UserId,
                ProductsId = Rentings.ProductsId,
                DateStart = Rentings.DateStart,
                DateFinished = Rentings.DateFinished,
                TotalPrice = PriceCalculated.ToString()
            };
            await _unitOfWork.Rentings.Upsert(RentingsToUpdate);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        } 

            [Authorization(Role.Admin)]
            [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var item = await _unitOfWork.Rentings.GetById(id);
            if(item == null)
            return BadRequest();

            await _unitOfWork.Rentings.Delete(id);
            await _unitOfWork.CompleteAsync();
            return Ok(item);
        } 
    }
}