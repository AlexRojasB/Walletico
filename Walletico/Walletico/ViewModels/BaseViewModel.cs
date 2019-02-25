using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Text;

namespace Walletico.ViewModels
{
    public class BaseViewModel : ReactiveObject, IRoutableViewModel, ISupportsActivation
    {
        public string UrlPathSegment
        {
            get;
            protected set;
        }

        public IScreen HostScreen
        {
            get;
            protected set;
        }

        public ViewModelActivator Activator => viewModelActivator;

        protected readonly ViewModelActivator viewModelActivator = new ViewModelActivator();

        public BaseViewModel(IScreen hostScreen = null)
        {
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
        }
    }
}
