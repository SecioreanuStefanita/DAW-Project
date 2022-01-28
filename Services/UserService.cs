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
using System.Threading.Tasks;

namespace Proiect.Services
{
    public class UserService : IUserService
    {

        public ApplicationDbContext _ApplicationDbContext;
        private IJWTUtils _iJWtUtils;
        private readonly AppSettings _appSettings;

        public UserService(ApplicationDbContext ApplicationDbContext, IJWTUtils iJWtUtils, IOptions<AppSettings> appSettings)
        {
            _ApplicationDbContext = ApplicationDbContext;
            _iJWtUtils = iJWtUtils;
            _appSettings = appSettings.Value;
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

        public User GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
