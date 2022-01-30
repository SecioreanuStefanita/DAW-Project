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
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUnitofWork _unitOfWork;

          private IUserService _userService;

        public UsersController(ILogger<UsersController> logger, IUnitofWork unitOfWork, IUserService userService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userService = userService;
        }



         [HttpPost("authentificate")]
        public IActionResult Authentificate(UserRequestDTO user)
        {
            var response = _userService.Authentificate(user);
             
            if( response == null)
            {
                return BadRequest(new { Message = "Username or Password is invalid!" });
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult>CreateUser(UserRequestDTO user)
        {
            var userToCreate = new User
            {
                FirstName = user.FirstName,
                Role = user.Role,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.Username,
                PasswordHash = BCryptNet.HashPassword(user.Password)
            };

            if(ModelState.IsValid)
            {
                await _unitOfWork.Users.Add(userToCreate);
                await _unitOfWork.CompleteAsync();
                return Ok();
            }

            return new JsonResult("Something is Wrong") {StatusCode= 500};
        }

            [Authorization(Role.Admin)]
             [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(Guid id)
        {         Console.WriteLine(id);
                var user = await _unitOfWork.Users.GetById(id);
                if(user == null){
                    return NotFound();
                }
               return Ok(user);
        }
            [Authorization(Role.Admin)]
            [HttpGet]
        public async Task<IActionResult> Get()
        {
                var user = await _unitOfWork.Users.All();
               return Ok(user);
        }

            [Authorization(Role.Admin)]
            [HttpPost("{id}")]
        public async Task<IActionResult> UpdateItem(Guid id, UserCRUDRequestDTO user)
        {    
              if(id != user.Id)
                    return BadRequest();
                  var userToUpdate = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.Username,
                Role = user.Role
            };
               

            await _unitOfWork.Users.Upsert(userToUpdate);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        } 

            [Authorization(Role.Admin)]
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



            [Authorization(Role.User)]
            [HttpGet("getAllData/{id}")]
        public async Task<IActionResult> GetAllData(Guid id)
        {
            var item = _userService.GetAllData(id);
            if(item == null)
            return BadRequest();
            await _unitOfWork.CompleteAsync();
            return Ok(item);
        }  
    }
}