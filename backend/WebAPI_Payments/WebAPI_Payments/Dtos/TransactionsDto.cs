using System;

namespace WebAPI_Payments.Dtos
{
    public class TransactionsDto
    {
        public int Id { get; set; }
        public string TypeString { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
