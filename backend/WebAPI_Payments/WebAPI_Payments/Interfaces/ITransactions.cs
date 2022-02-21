using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI_Payments.Models;

namespace WebAPI_Payments.Interfaces
{
    public interface ITransactions
    {
        Task<List<TransactionsHistory>> GetTransactionsAsync(int userId);
        void AddTransaction(int userId, double amount, DateTime date, TransactionType type);
    }
}
