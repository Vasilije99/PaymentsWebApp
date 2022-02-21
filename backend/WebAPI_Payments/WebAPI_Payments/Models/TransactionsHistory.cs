using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI_Payments.Models
{
    public static class StringExtensions
    {
        public static T ParseEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
    public enum TransactionType { DEPOSIT = 1, WITHDRAWAL = 2 };
    public class TransactionsHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }

        [Column("Type")]
        public string TypeString
        {
            get { return TransactionType.ToString(); }
            private set { TransactionType = value.ParseEnum<TransactionType>(); }
        }

        [NotMapped]
        public TransactionType TransactionType { get; set; }
    }
}
