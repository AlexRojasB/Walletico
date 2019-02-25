using ReactiveUI;
using ReactiveUI.XamForms;
using Splat;
using System;
using Walletico.DataServices;
using Walletico.Pages;
using Walletico.ViewModels;
using Xamarin.Forms;

namespace Walletico
{
    public class AppBootstrapper : ReactiveObject, IScreen
    {
        public RoutingState Router { get; protected set; }

        public AppBootstrapper()
        {
            Router = new RoutingState();
            Locator.CurrentMutable.RegisterConstant(this, typeof(IScreen));
            Locator.CurrentMutable.Register(() => new DataService(), typeof(IDataService));
            Locator.CurrentMutable.Register(() => new DetailListPage(), typeof(IViewFor<DetailListViewModel>));
            this
                .Router
                .NavigateAndReset
                .Execute(new DetailListViewModel(Locator.CurrentMutable.GetService<IDataService>()))
                .Subscribe();
        }

        public Page CreateMainPage()
        {
            // NB: This returns the opening page that the platform-specific
            // boilerplate code will look for. It will know to find us because
            // we've registered our AppBootstrappScreen.
            return new RoutedViewHost();
        }
    }

}
