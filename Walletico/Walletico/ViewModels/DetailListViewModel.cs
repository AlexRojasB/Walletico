using ReactiveUI;
using System.Collections.Generic;
using System.Linq;
using Walletico.DataServices;
using Walletico.Models;

namespace Walletico.ViewModels
{
    public class DetailListViewModel : BaseViewModel
    {
        private Transaction _transactionSelected;
        private Period _periodSelected;
        private readonly IDataService _dataService;

        public DetailListViewModel(IDataService dataService, IScreen hostScreen = null): base(hostScreen)
        {
            this._dataService = dataService;
            this.Transactions = this._dataService.GetAllPerMonthTransactions(1).ToList();
            this.Periods = this._dataService.GetAllMonths().ToList();
        }

        #region Properties
        public List<Transaction> Transactions { get; set; }
        

        public List<Period> Periods { get; set; }

        public Period PeriodSelected
        {
            get => _periodSelected;
            set {
                foreach (Period period in Periods.Where(x => x.IsSelected))
                {
                    period.IsSelected = false;
                }
                _periodSelected.IsSelected = true;
                this.RaiseAndSetIfChanged(ref _periodSelected, value);

            }
        }

        public Transaction TransactionSelected
        {
            get => _transactionSelected;
            set
            {
                foreach (Transaction item in this.Transactions.Where(x => x.IsSelected))
                {
                    item.IsSelected = false;
                }
                _transactionSelected.IsSelected = true;
                this.RaiseAndSetIfChanged(ref _transactionSelected, value);
            }
        }
        #endregion
    }
}
