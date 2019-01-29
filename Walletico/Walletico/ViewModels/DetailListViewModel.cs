using System;
using System.Collections.Generic;
using Walletico.Models;

namespace Walletico.ViewModels
{
    public class DetailListViewModel : FreshMvvm.FreshBasePageModel
    {
        public DetailListViewModel()
        {
            this.Transactions = new List<Transaction>
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

        #region Properties
        public List<Transaction> Transactions { get; set; }
        #endregion
    }
}
