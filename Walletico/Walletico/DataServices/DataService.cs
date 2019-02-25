using System;
using System.Collections.Generic;
using Walletico.Models;

namespace Walletico.DataServices
{
    public class DataService : IDataService
    {
        public IEnumerable<Period> GetAllMonths() => new List<Period>
            {
                new Period
                {
                    Month = "JAN"
                },new Period
                {
                    Month = "FEB"
                },
                new Period
                {
                    Month = "MAR"
                },
                new Period
                {
                    Month = "APR"
                },
                new Period
                {
                    Month = "MAY"
                },
                new Period
                {
                    Month = "JUN"
                },
                new Period
                {
                    Month = "JUL"
                },
                new Period
                {
                    Month = "AGO"
                },
                new Period
                {
                    Month = "SET"
                },
                new Period
                {
                    Month = "OCT"
                },
                new Period
                {
                    Month = "NOV"
                },
                new Period
                {
                    Month = "DIC"
                },
            };


        public IEnumerable<Transaction> GetAllPerMonthTransactions(int month) => new List<Transaction>
            {
                new Transaction
                {
                    Amount = 1200.63M,
                    Category = "",
                    Description = "Gasolina",
                    EntryDate = DateTime.Now.AddMinutes(15)
                },
                new Transaction
                {
                    Amount = 8500.74M,
                    Category = "",
                    Description = "Restaurante Polli",
                    EntryDate = DateTime.Now.AddMonths(2)
                },
            };
    }
}
