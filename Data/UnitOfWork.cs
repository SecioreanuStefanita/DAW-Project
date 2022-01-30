using System;
using Proiect.Core.IConfig;
using Proiect.Core.IRepositories;
using Proiect.Core.Repositories;

namespace Proiect.Data
{
    public class UnitOfWork: IUnitofWork
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

     

        public IUserRepository Users{get; private set;}

        public IProduseRepository Produse {get; private set;}

         public IOrdersRepository Orders{get; private set;}

        public IRentingsRepository Rentings{get; private set;}

        public IUserDetailsRepository UserDetails{get; private set;}


        public UnitOfWork(ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");

            Users = new UserRepository(_context,_logger);
            Produse = new ProduseRepository(_context,_logger);
            Orders = new OrdersRepository(_context,_logger);
            Rentings = new RentingsRepository(_context,_logger);
            UserDetails = new UserDetailsRepository(_context,_logger);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();

        }

    //   public void Dispose()
    //     {
    //         _context.Dispose();
    //     }
    }
}
