using AiForms.Dialogs;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Walletico.CustomViews;
using Walletico.Shared;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Walletico.ViewModels.CustomViewModels
{
    public class AddTransactionPopupViewModel : FreshMvvm.FreshBasePageModel
    {
        public AddTransactionPopupViewModel()
        {
            this.ReadAndReconfigureLocationPreferences();
            this.IsLocationEnabled = Preferences.Get(PreferenceKeys.IsLocationEnabled, false);
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
                        var ret = await Dialog.Instance.ShowAsync<LocationDialog>();
                    }
                }

                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
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

        public ICommand EnableLocation
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
