using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Walletico.Models;

namespace Walletico.DataServices
{
    public class DataService : IDataService
    {
        public IEnumerable<Period> GetAllMonths() => 
            DateTimeFormatInfo.CurrentInfo.AbbreviatedMonthNames.Where(c => !string.IsNullOrEmpty(c)).Select(c => new Period { Month = c.Replace(".", "").ToUpperInvariant() }).ToList();

        public IEnumerable<Transaction> GetAllPerMonthTransactions(int month) => new List<Transaction>
            {
                new Transaction
                {
                    Amount = 1200.63M,
                    Category = null,
                    Description = "Gasolina",
                    EntryDate = DateTime.Now.AddMinutes(15)
                },
                new Transaction
                {
                    Amount = 8500.74M,
                    Category = null,
                    Description = "Restaurante Polli",
                    EntryDate = DateTime.Now.AddMonths(2)
                },
            };
    }
}
