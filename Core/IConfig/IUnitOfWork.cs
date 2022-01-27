using Proiect.Models;
using Proiect.Core.IRepositories;
namespace Proiect.Core.IConfig

{
    public interface IUnitofWork
    {
               IUserRepository Users {get;}
               Task CompleteAsync();
    }
}