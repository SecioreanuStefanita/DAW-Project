using Proiect.Models;
namespace Proiect.Core.IRepositories

{
    public interface IUserRepository : IGenericRepository<User>
    {
                Task<string> GetFirstNameAndLastName(Guid id);
    }
}