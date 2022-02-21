using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_Payments.Data.Repo;
using WebAPI_Payments.Interfaces;

namespace WebAPI_Payments.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext dc;

        public UnitOfWork(DataContext dc)
        {
            this.dc = dc;
        }

        public IUserRepository UserRepository => 
            new UserRepository(dc);

        public ITransactions TransactionsRepository =>
            new TransactionsRepository(dc);

        public async Task<bool> SaveAsync()
        {
            return await dc.SaveChangesAsync() > 0;
        }
    }
}
