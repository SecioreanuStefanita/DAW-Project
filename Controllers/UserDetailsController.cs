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
    public class UserDetailsController : ControllerBase
    {
        private readonly ILogger<UserDetailsController> _logger;
        private readonly IUnitofWork _unitOfWork;

          private IUserDetailsService _UserDetailservice;

        public UserDetailsController(ILogger<UserDetailsController> logger, IUnitofWork unitOfWork, IUserDetailsService UserDetailservice)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _UserDetailservice = UserDetailservice;
        }


        [HttpPost]
        public async Task<IActionResult>CreateUserDetails(UserDetailsRequestDTO UserDetails)
        {
            var UserDetailsToCreate = new UserDetails
            {
                Address = UserDetails.Address,
                PhoneNumber = UserDetails.PhoneNumber,
                BusinessDetails  = UserDetails.BusinessDetails ,
                UserId  = UserDetails.UserId 
            };

            if(ModelState.IsValid)
            {
                await _unitOfWork.UserDetails.Add(UserDetailsToCreate);
                await _unitOfWork.CompleteAsync();
                return Ok();
            }

            return new JsonResult("Something is Wrong") {StatusCode= 500};
        }

            [Authorization(Role.Admin)]
             [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(Guid id)
        {   
                var UserDetails = await _unitOfWork.UserDetails.GetById(id);
                if(UserDetails == null){
                    return NotFound();
                }
               return Ok(UserDetails);
        }
            [Authorization(Role.Admin)]
            [HttpGet]
        public async Task<IActionResult> Get()
        {
                var UserDetails = await _unitOfWork.UserDetails.All();
               return Ok(UserDetails);
        }

            [Authorization(Role.Admin)]
            [HttpPost("{id}")]
        public async Task<IActionResult> UpdateItem(Guid id, UserDetailsRequestDTO UserDetails)
        {    
              if(id != UserDetails.Id)
                    return BadRequest();
                      var userDetailsToUpdate = new UserDetails
            {
                Id = UserDetails.Id,
                Address = UserDetails.Address,
                PhoneNumber = UserDetails.PhoneNumber,
                BusinessDetails = UserDetails.BusinessDetails,
                UserId = UserDetails.UserId
            };
               

            await _unitOfWork.UserDetails.Upsert(userDetailsToUpdate);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        } 

            [Authorization(Role.Admin)]
            [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var item = await _unitOfWork.UserDetails.GetById(id);
            if(item == null)
            return BadRequest();

            await _unitOfWork.UserDetails.Delete(id);
            await _unitOfWork.CompleteAsync();
            return Ok(item);
        } 
    }
}