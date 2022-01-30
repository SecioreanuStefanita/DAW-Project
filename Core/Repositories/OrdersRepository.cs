using Proiect.Core.IRepositories;
using Proiect.Data;
using Microsoft.EntityFrameworkCore;
using Proiect.Models;

namespace Proiect.Core.Repositories
{
    public class OrdersRepository : GenericRepository<Orders>, IOrdersRepository
    {
        public OrdersRepository(
            ApplicationDbContext context,
            ILogger logger
        ): base(context,logger)
        {

        }

        public override async  Task<IEnumerable<Orders>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch( Exception ex)
            {
                _logger.LogError(ex,"{Repo} All method error", typeof(OrdersRepository));
                return new List<Orders>();
            }
        }

            public override async  Task<bool> Upsert(Orders entity)
        {   
             try
            {
            var existingOrders = await dbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();
            if(existingOrders == null)
                return await Add(entity);
            
            existingOrders.UserId = entity.UserId;
            existingOrders.Id = entity.Id;
            existingOrders.Price = entity.Price;
            existingOrders.ProductsId = entity.ProductsId;
            return true;
            }
            catch( Exception ex)
            {
                _logger.LogError(ex,"{Repo} Upsert method error", typeof(OrdersRepository));
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
                   _logger.LogError(ex,"{Repo} Upsert method error", typeof(OrdersRepository));
                return false;
            }
        }

        public Task<string> GetFirstNameAndLastName(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}