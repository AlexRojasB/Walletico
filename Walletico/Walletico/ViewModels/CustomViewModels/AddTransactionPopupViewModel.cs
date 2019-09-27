using AiForms.Dialogs;
using System;
using System.Linq;
using System.Threading.Tasks;
using Walletico.CustomViews;
using Walletico.Service;
using Walletico.Shared;
using Walletico.Shared.BoundaryHelper;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Walletico.ViewModels.CustomViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class AddTransactionPopupViewModel : FreshMvvm.FreshBasePageModel
    {
        private readonly IMapService _mapService;
        private readonly bool _isAllowed;
        public AddTransactionPopupViewModel(IMapService mapService)
        {
            this._mapService = mapService;
            this.IsLocationEnabled = Preferences.Get(PreferenceKeys.IsLocationEnabled, false);
            _isAllowed = Preferences.Get(PreferenceKeys.IsLocationAllowed, false);
            this.ReadAndReconfigureLocationPreferences();
        }
        public AddTransactionPopupViewModel()
        {
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
                        var placesNames = places.Select(x => x.Place_name).ToArray();
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
                }
            } catch (Exception)
            {
                this.DisableLocationPreferences();
            }
        }

        private async void ReadAndReconfigureLocationPreferences()
        {
            if (_isAllowed)
            {
                if (this.IsLocationEnabled)
                {
                    await this.VerifyGpsLocation();
                }
            }
            else
            {
                Preferences.Set(PreferenceKeys.IsLocationEnabled, false);
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

        public bool IsLocationEnabled { get; set; }
    }
}
