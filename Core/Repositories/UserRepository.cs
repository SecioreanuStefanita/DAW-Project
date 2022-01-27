using Proiect.Core.IRepositories;
using Proiect.Data;
using Microsoft.EntityFrameworkCore;
using Proiect.Models;

namespace Proiect.Core.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(
            ApplicationDbContext context,
            ILogger logger
        ): base(context,logger)
        {

        }

        public override async  Task<IEnumerable<User>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch( Exception ex)
            {
                _logger.LogError(ex,"{Repo} All method error", typeof(UserRepository));
                return new List<User>();
            }
        }

            public override async  Task<bool> Upsert(User entity)
        {   
             try
            {
                    var existingUser = await dbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();
            if(existingUser == null)
                return await Add(entity);
            
            existingUser.FirstName = entity.FirstName;
            existingUser.Email = entity.Email;
            existingUser.LastName = entity.LastName;
            return true;
            }
            catch( Exception ex)
            {
                _logger.LogError(ex,"{Repo} Upsert method error", typeof(UserRepository));
                return false;
            }
        }

        public  override async  Task<bool> Delete(Guid Id)
        {
            try
            {
                var exist = await dbSet.Where(x=>x.Id==Id).FirstOrDefaultAsync();
                if(exist != null){
                    dbSet.Remove(exist);
                    return true;
                }

                return false;
            }
            catch(Exception ex)
            {
                   _logger.LogError(ex,"{Repo} Upsert method error", typeof(UserRepository));
                return false;
            }
        }

        public Task<string> GetFirstNameAndLastName(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}