using AiForms.Dialogs;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
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
        private IMapService _mapService;
        public AddTransactionPopupViewModel(IMapService mapService)
        {
            this._mapService = mapService;
            this.ReadAndReconfigureLocationPreferences();
            this.IsLocationEnabled = Preferences.Get(PreferenceKeys.IsLocationEnabled, false);
        }
        public AddTransactionPopupViewModel()
        {

        }
        private async Task VerifyGpsLocation()
        {
            try
            {
                bool isAllowed = Preferences.Get(PreferenceKeys.IsLocationAllowed, false);
                this.IsLocationEnabled = !this.IsLocationEnabled;
                if (this.IsLocationEnabled)
                {
                    if (!isAllowed)
                    {
                        await Dialog.Instance.ShowAsync<LocationDialog>();
                    }
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
                }
                else
                {
                    this.DisableLocationPreferences();
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                this.DisableLocationPreferences();
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                this.DisableLocationPreferences();

            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                this.DisableLocationPreferences();

            }
            catch (Exception ex)
            {
                // Unable to get location
                this.DisableLocationPreferences();
            }
        }

        private void ReadAndReconfigureLocationPreferences()
        {
            bool isAllowed = Preferences.Get(PreferenceKeys.IsLocationAllowed, false);

            if (!isAllowed)
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
                    await this.VerifyGpsLocation();
                });

            }
        }

        public bool IsLocationEnabled { get; set; }
    }
}
