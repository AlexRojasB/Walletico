using AiForms.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Walletico.CustomViews;
using Walletico.DataServices;
using Walletico.Models;
using Walletico.Models.Base;
using Walletico.Service;
using Walletico.Shared;
using Walletico.Shared.BoundaryHelper;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Walletico.ViewModels
{
    public class DetailListViewModel : FreshMvvm.FreshBasePageModel
    {
        private Transaction _transactionSelected;
        private Period _periodSelected;
        private decimal income;
        private bool isLocationEnabled;
        private IEnumerable<Geoplace> nearPlaces;
        private readonly IDataService _dataService;
        private readonly IMapService _mapService;
        private readonly bool _isAllowed;

        public DetailListViewModel()
        {
            this.Transactions = Enumerable.Empty<Transaction>();
        }
        public DetailListViewModel(IDataService dataService, IMapService mapService)
        {
            this._dataService = dataService;
            this._mapService = mapService;
            this.IsLocationEnabled = Preferences.Get(PreferenceKeys.IsLocationEnabled, false);
            _isAllowed = Preferences.Get(PreferenceKeys.IsLocationAllowed, false);
            this.ReadAndReconfigureLocationPreferences();
            this.Transactions = this._dataService.GetAllPerMonthTransactions(1).ToList();
            this.Income = this.Transactions.Where(t => t.TransType == 0).Sum(t => t.Amount);
            this.Periods = this._dataService.GetAllMonths().ToList();
        }

        private void ReadAndReconfigureLocationPreferences()
        {
            if (!_isAllowed)
            {
                Preferences.Set(PreferenceKeys.IsLocationEnabled, false);
            }
        }

        private async Task VerifyGpsLocation()
        {
            try
            {
                if (this.IsLocationEnabled)
                {
                    if (!_isAllowed)
                    {
                        await Dialog.Instance.ShowAsync<LocationDialog>();
                    }
                    var location = await Geolocation.GetLastKnownLocationAsync();

                    if (location != null)
                    {
                        MapPoint userLocation = new MapPoint
                        {
                            Latitude = location.Latitude,
                            Longitude = location.Longitude
                        };
                        var places = await this._mapService.GetPlacesNearby(userLocation, 1.5d);
                        this.NearPlaces = places.Select(x => new Geoplace { PlaceName = x.Text, Coordenates = new MapPoint { Latitude = x.Geometry.Coordinates[0], Longitude = x.Geometry.Coordinates[1] } }).ToArray();
                        Preferences.Set(PreferenceKeys.IsLocationAllowed, true);
                        Preferences.Set(PreferenceKeys.IsLocationEnabled, true);
                    }
                    else
                    {
                        this.DisableLocationPreferences();
                    }
                }
                else
                {
                    Preferences.Set(PreferenceKeys.IsLocationEnabled, false);
                    this.NearPlaces = Enumerable.Repeat(new Geoplace { PlaceName = "Somewhere", Coordenates = new MapPoint { Latitude = 0, Longitude = 0 } }, 1);
                }
            }
            catch (Exception)
            {
                this.DisableLocationPreferences();
            }
        }


        private void DisableLocationPreferences()
        {
            Preferences.Set(PreferenceKeys.IsLocationEnabled, false);
            Preferences.Set(PreferenceKeys.IsLocationAllowed, false);
        }

        public Command EnableLocation
        {
            get
            {
                return new Command(async () =>
                {
                    this.IsLocationEnabled = !this.IsLocationEnabled;
                    await this.VerifyGpsLocation();
                });

            }
        }

        public Command OpenPopup => new Command(async () => { await this.VerifyGpsLocation(); });


        #region Properties
        public IEnumerable<Transaction> Transactions { get; set; }


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

        public decimal Income
        {
            get => income; set
            {
                income = value;
                this.RaisePropertyChanged();
            }
        }

        public decimal Outcome => this.Transactions.Where(t => t.TransType == 1).Sum(t => t.Amount);

        public decimal Current => this.Income - this.Outcome;
        public bool IsLocationEnabled
        {
            get => isLocationEnabled; set
            {
                isLocationEnabled = value;
                this.RaisePropertyChanged();
            }
        }
        public IEnumerable<Geoplace> NearPlaces
        {
            get => nearPlaces;
            set
            {
                nearPlaces = value;
                RaisePropertyChanged();
            }
        }
        #endregion
    }
}
