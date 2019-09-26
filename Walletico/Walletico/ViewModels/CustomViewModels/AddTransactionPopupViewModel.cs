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
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class AddTransactionPopupViewModel : FreshMvvm.FreshBasePageModel
    {
        private readonly bool _isAllowed;
        public AddTransactionPopupViewModel()
        {
            this.IsLocationEnabled = Preferences.Get(PreferenceKeys.IsLocationEnabled, false);
            _isAllowed = Preferences.Get(PreferenceKeys.IsLocationAllowed, false);
            this.ReadAndReconfigureLocationPreferences();
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
                        Preferences.Set(PreferenceKeys.IsLocationAllowed, true);
                        Preferences.Set(PreferenceKeys.IsLocationEnabled, true);
                        Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
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
