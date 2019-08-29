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
            await AddTransPopup.TranslateTo(0, Height, AnimationSpeed, Easing.SinInOut);
            await PageFader.FadeTo(0, AnimationSpeed, Easing.SinInOut);
            PageFader.IsVisible = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.AddTransPopup.OnExpandTapped += ExpandPopup;
        }

        private void ExpandPopup()
        {
            var height = this.AddTransPopup.Height;
            this.AddTransPopup.TranslateTo(0, this.Height - height, AnimationSpeed, Easing.SinInOut);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            this.AddTransPopup.OnExpandTapped -= ExpandPopup;
        }

        private async void IncomeTotalTapped(object sender, System.EventArgs e)
        {
            var pageHeight = Height;
            var firstSection = AddTransPopup.IncomeFirstSectionHeigh;
            PageFader.IsVisible = true;
            await PageFader.FadeTo(1, AnimationSpeed, Easing.SinInOut);
            await AddTransPopup.TranslateTo(0, pageHeight - firstSection, AnimationSpeed, Easing.SinInOut);
        }
    }
}