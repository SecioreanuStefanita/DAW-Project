using Proiect.Models;
using Proiect.Core.IRepositories;
namespace Proiect.Core.IConfig

{
    public interface IUnitofWork
    {
               IUserRepository Users {get;}
               IProduseRepository Produse {get;}

               IRentingsRepository Rentings {get;}
               IOrdersRepository Orders {get;}
               IUserDetailsRepository UserDetails {get;}
               Task CompleteAsync();
    }
}