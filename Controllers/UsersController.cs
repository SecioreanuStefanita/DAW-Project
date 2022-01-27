using Microsoft.AspNetCore.Mvc;
using Proiect.Core.IConfig;
using Proiect.Models;
namespace Proiect.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUnitofWork _unitOfWork;

        public UsersController(ILogger<UsersController> logger, IUnitofWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            if(ModelState.IsValid)
            {
                user.Id = Guid.NewGuid();

                await _unitOfWork.Users.Add(user);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetItem", new {user.Id}, user);
            }

            return new JsonResult("Something is Wrong") {StatusCode= 500};
        }

             [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(Guid id)
        {
                var user = await _unitOfWork.Users.GetById(id);
                if(user == null){
                    return NotFound();
                }
               return Ok(user);
        }

            [HttpGet]
        public async Task<IActionResult> Get()
        {
                var user = await _unitOfWork.Users.All();
               return Ok(user);
        }

            [HttpPost("{id}")]
        public async Task<IActionResult> UpdateItem(Guid id, User user)
        {
              if(id != user.Id)
                    return BadRequest();
            await _unitOfWork.Users.Upsert(user);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        } 

            [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var item = await _unitOfWork.Users.GetById(id);
            if(item == null)
            return BadRequest();

            await _unitOfWork.Users.Delete(id);
            await _unitOfWork.CompleteAsync();
            return Ok(item);
        } 
    }
}