using System;
using System.Collections.Generic;
using System.Linq;
using Walletico.Models;

namespace Walletico.ViewModels
{
    public class DetailListViewModel : FreshMvvm.FreshBasePageModel
    {
        private Transaction transactionSelected;
        private Period periodSelected;

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
            this.Periods = new List<Period>
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
        }

        #region Properties
        public List<Transaction> Transactions { get; set; }
        

        public List<Period> Periods { get; set; }

        public Period PeriodSelected
        {
            get => periodSelected;
            set {
                periodSelected = value;
                foreach (Period period in Periods.Where(x => x.IsSelected))
                {
                    period.IsSelected = false;
                }
                periodSelected.IsSelected = true;
                RaisePropertyChanged();
            }
        }

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
