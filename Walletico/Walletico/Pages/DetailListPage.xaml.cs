using AiForms.Dialogs;
using Walletico.CustomViews;
using Walletico.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Walletico.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailListPage : ContentPage
    {

        const uint AnimationSpeed = 300;
        public DetailListPage()
        {
            InitializeComponent();
        }

        private async void PageFader_Tapped(object sender, System.EventArgs e)
        {
            await AddTransPopup.TranslateTo(0, Height, AnimationSpeed, Easing.SinInOut).ConfigureAwait(true);
            await PageFader.FadeTo(0, AnimationSpeed, Easing.SinInOut).ConfigureAwait(true);
            PageFader.IsVisible = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.AddTransPopup.OnExpandTapped += ExpandPopup;
            //this.AddTransPopup.OnAddTransactionClicked += AddTransactionClicked;
        }

       

        private void ExpandPopup()
        {
            var height = this.AddTransPopup.Height;
            this.AddTransPopup.TranslateTo(0, this.Height - height, AnimationSpeed, Easing.SinInOut);
        }

        private void AddTransactionClicked(Transaction transaction)
        {

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            this.AddTransPopup.OnExpandTapped -= ExpandPopup;
            //this.AddTransPopup.OnAddTransactionClicked -= AddTransactionClicked;
        }

        private async void IncomeTotalTapped(object sender, System.EventArgs e)
        {
            var pageHeight = Height;
            var firstSection = AddTransPopup.IncomeFirstSectionHeigh;
            PageFader.IsVisible = true;
            await PageFader.FadeTo(1, AnimationSpeed, Easing.SinInOut).ConfigureAwait(false);
            await AddTransPopup.TranslateTo(0, pageHeight - firstSection, AnimationSpeed, Easing.SinInOut).ConfigureAwait(false);
        }

        private void OutcomeTotalTapped(object sender, System.EventArgs e)
        {
            
        }
    }
}