using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_Payments.Interfaces;
using WebAPI_Payments.Models;

namespace WebAPI_Payments.Data.Repo
{
    public class TransactionsRepository : ITransactions
    {
        private readonly DataContext dc;

        public TransactionsRepository(DataContext dc)
        {
            this.dc = dc;
        }

        public void AddTransaction(int userId, double amount, DateTime date, TransactionType type)
        {
            TransactionsHistory transaction = new TransactionsHistory();
            transaction.UserId = userId;
            transaction.Amount = amount;
            transaction.Date = date;
            transaction.TransactionType = type;

            dc.Transactions.Add(transaction);
        }

        public async Task<List<TransactionsHistory>> GetTransactionsAsync(int userId)
        {
            var transactions = await dc.Transactions.Where(item => item.UserId == userId).ToListAsync();

            return transactions;
        }
    }
}
