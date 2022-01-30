using Proiect.Core.IRepositories;
using Proiect.Data;
using Microsoft.EntityFrameworkCore;
using Proiect.Models;

namespace Proiect.Core.Repositories
{
    public class RentingsRepository : GenericRepository<Rentings>, IRentingsRepository
    {
        public RentingsRepository(
            ApplicationDbContext context,
            ILogger logger
        ): base(context,logger)
        {

        }


        public override async  Task<IEnumerable<Rentings>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch( Exception ex)
            {
                _logger.LogError(ex,"{Repo} All method error", typeof(RentingsRepository));
                return new List<Rentings>();
            }
        }

            public override async  Task<bool> Upsert(Rentings entity)
        {   
             try
            {
            var existingRentings = await dbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();
            if(existingRentings == null)
                return await Add(entity);
            
            existingRentings.DateFinished = entity.DateFinished;
            existingRentings.DateStart = entity.DateStart;
            existingRentings.UserId = entity.UserId;
            existingRentings.ProductsId = entity.ProductsId;
            existingRentings.TotalPrice = entity.TotalPrice;
            return true;
            }
            catch( Exception ex)
            {
                _logger.LogError(ex,"{Repo} Upsert method error", typeof(RentingsRepository));
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
                   _logger.LogError(ex,"{Repo} Upsert method error", typeof(RentingsRepository));
                return false;
            }
        }

        public Task<string> GetFirstNameAndLastName(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}