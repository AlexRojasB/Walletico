using FreshMvvm;
using Walletico.DataServices;
using Walletico.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Walletico
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();;
            this.SetupIOC();
            var page = FreshPageModelResolver.ResolvePageModel<DetailListViewModel>();
            MainPage = new FreshNavigationContainer(page);
        }

        private void SetupIOC()
        {
            FreshIOC.Container.Register<IDataService, DataService>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
