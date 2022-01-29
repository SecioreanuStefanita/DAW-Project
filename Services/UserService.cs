using Proiect.Data;
using Proiect.Models;
using Proiect.Models.DTOs;
using Proiect.Utilities;
using Proiect.Utilities.JWTUtilis;
using BCryptNet = BCrypt.Net.BCrypt;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using Proiect.Core.IRepositories;
using System.Threading.Tasks;
using Proiect.Core.IConfig;
namespace Proiect.Services
{
    public class UserService : IUserService
    {

        public ApplicationDbContext _ApplicationDbContext;
        private IJWTUtils _iJWtUtils;

        private readonly IUnitofWork _unitOfWork;


        public UserService(ApplicationDbContext ApplicationDbContext, IJWTUtils iJWtUtils,  IUnitofWork unitOfWork)
        {
            _ApplicationDbContext = ApplicationDbContext;
            _iJWtUtils = iJWtUtils;
            _unitOfWork = unitOfWork;
        }


        public UserResponseDTO Authentificate(UserRequestDTO model)
        {
            var user = _ApplicationDbContext.Users.FirstOrDefault(x => x.Username == model.Username);

            if(user == null || !BCryptNet.Verify(model.Password, user.PasswordHash))
            {
                return null; //or throw exception
            }

            // jwt generation
            var jwtToken = _iJWtUtils.GenerateJWTToken(user);
            return new UserResponseDTO(user, jwtToken);
        }

        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        async Task<User> IUserService.GetById(Guid id)
        {
            var user = await _unitOfWork.Users.GetById(id);
                if(user == null){
                    return null;
                }
               return user;
        }
    }
}
