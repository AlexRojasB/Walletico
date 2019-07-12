using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Walletico.DataServices;
using Walletico.Models;
using Walletico.Models.Base;

namespace Walletico.ViewModels
{
    public class DetailListViewModel : FreshMvvm.FreshBasePageModel
    {
        private Transaction _transactionSelected;
        private Period _periodSelected;
        private IEnumerable<Transaction> _transactions;
        private readonly IDataService _dataService;
        public DetailListViewModel()
        {

        }
        public DetailListViewModel(IDataService dataService)
        {
            this._dataService = dataService;
            this.Transactions = this._dataService.GetAllPerMonthTransactions(1).ToList();
            this.Periods = this._dataService.GetAllMonths().ToList();
        }

        #region Properties
        public IEnumerable<Transaction> Transactions { get => _transactions ?? (_transactions = Enumerable.Empty<Transaction>()); set => _transactions = value; }


        public List<Period> Periods { get; set; }

        public Period PeriodSelected
        {
            get => _periodSelected;
            set
            {
                this.ChangeSelectedStatus(this.Periods);
                _periodSelected = value;
                _periodSelected.IsSelected = true;
                this.RaisePropertyChanged();
            }
        }

        protected void ChangeSelectedStatus(IEnumerable<ISelectable> list)
        {
            foreach (ISelectable selectable in list.Where(x => x.IsSelected))
            {
                selectable.IsSelected = false;
            }
        }

        public Transaction TransactionSelected
        {
            get => _transactionSelected;
            set
            {
                this.ChangeSelectedStatus(this.Transactions);
                _transactionSelected = value;
                _transactionSelected.IsSelected = true;
                this.RaisePropertyChanged();
            }
        }

        public decimal Income => this.Transactions.Where(t => t.TransType == 0).Sum(t => t.Amount);
        public decimal Outcome => this.Transactions.Where(t => t.TransType == 1).Sum(t => t.Amount);

        public decimal Current => this.Income - this.Outcome;
        #endregion
    }
}
