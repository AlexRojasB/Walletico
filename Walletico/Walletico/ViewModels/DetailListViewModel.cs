using System;
using System.Collections.Generic;
using System.Linq;
using Walletico.Models;

namespace Walletico.ViewModels
{
    public class DetailListViewModel : FreshMvvm.FreshBasePageModel
    {
        private Transaction transactionSelected;

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

        public Transaction TransactionSelected
        {
            get => transactionSelected;
            set
            {
                transactionSelected = value;
                foreach (Transaction item in this.Transactions.Where(x => x.IsSelected))
                {
                    item.IsSelected = false;
                }
                transactionSelected.IsSelected = true;
                RaisePropertyChanged();
            }
        }
        #endregion
    }
}
