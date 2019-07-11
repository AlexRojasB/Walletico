﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
         

            Observable.FromEventPattern<PropertyChangedEventArgs>(this, nameof(PropertyChanged))
                .Where(x => x.EventArgs.PropertyName == nameof(this.PeriodSelected) || x.EventArgs.PropertyName == nameof(this.TransactionSelected))
                .Select(
                _ =>
                {
                    if (_.EventArgs.PropertyName == nameof(this.PeriodSelected))
                        this.ChangeSelectedStatus(this.Periods);
                    else
                        this.ChangeSelectedStatus(this.Transactions);
                    return _.EventArgs.PropertyName == nameof(this.PeriodSelected);
                }).Subscribe((isPeriod) => { if (isPeriod) this.PeriodSelected.IsSelected = true; else this.TransactionSelected.IsSelected = true; });
        }

        #region Properties
        public IEnumerable<Transaction> Transactions { get => _transactions ?? (_transactions = Enumerable.Empty<Transaction>()); set => _transactions = value; }


        public List<Period> Periods { get; set; }

        public Period PeriodSelected
        {
            get => _periodSelected;
            set
            {
                _periodSelected = value;
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
                _transactionSelected = value;
                this.RaisePropertyChanged();
            }
        }

        public decimal Income => this.Transactions.Where(t => t.TransType == 0).Sum(t => t.Amount);
        public decimal Outcome => this.Transactions.Where(t => t.TransType == 1).Sum(t => t.Amount);

        public decimal Current => this.Income - this.Outcome;
        #endregion
    }
}
