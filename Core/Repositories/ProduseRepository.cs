using Proiect.Core.IRepositories;
using Proiect.Data;
using Microsoft.EntityFrameworkCore;
using Proiect.Models;

namespace Proiect.Core.Repositories
{
    public class ProduseRepository : GenericRepository<Products>, IProduseRepository
    {
        public ProduseRepository(
            ApplicationDbContext context,
            ILogger logger
        ): base(context,logger)
        {

        }

        public override async  Task<IEnumerable<Products>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch( Exception ex)
            {
                _logger.LogError(ex,"{Repo} All method error", typeof(UserRepository));
                return new List<Products>();
            }
        }

            public override async  Task<bool> Upsert(Products entity)
        {   
             try
            {
            var existingProduct = await dbSet.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();
            if(existingProduct == null)
                return await Add(entity);
            
            existingProduct.Description = entity.Description;
            existingProduct.Name = entity.Name;
            existingProduct.Price = entity.Price;
            existingProduct.Id = entity.Id;
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