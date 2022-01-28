using Proiect.Models;
using Proiect.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.Services
{
    public interface IUserService
    {
        UserResponseDTO Authentificate(UserRequestDTO model);
        IEnumerable<User> GetAllUsers();
        Task<User> GetById(Guid id);
    }
}
