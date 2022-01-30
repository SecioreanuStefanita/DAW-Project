using Proiect.Core.IRepositories;
using Proiect.Data;
using Microsoft.EntityFrameworkCore;
using Proiect.Models;

namespace Proiect.Core.Repositories
{
    public class UserDetailsRepository : GenericRepository<UserDetails>, IUserDetailsRepository
    {
        public UserDetailsRepository(
            ApplicationDbContext context,
            ILogger logger
        ): base(context,logger)
        {

        }

        public override async  Task<IEnumerable<UserDetails>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch( Exception ex)
            {
                _logger.LogError(ex,"{Repo} All method error", typeof(UserDetailsRepository));
                return new List<UserDetails>();
            }
        }

            public override async  Task<bool> Upsert(UserDetails entity)
        {   
             try
            {
            var existingUserDetails = await dbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();
            if(existingUserDetails == null)
                return await Add(entity);
            
            existingUserDetails.Address = entity.Address;
            existingUserDetails.BusinessDetails = entity.BusinessDetails;
            existingUserDetails.PhoneNumber = entity.PhoneNumber;
            existingUserDetails.UserId = entity.UserId;
            return true;
            }
            catch( Exception ex)
            {
                _logger.LogError(ex,"{Repo} Upsert method error", typeof(UserDetailsRepository));
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
                   _logger.LogError(ex,"{Repo} Upsert method error", typeof(UserDetailsRepository));
                return false;
            }
        }

        public Task<string> GetFirstNameAndLastName(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}