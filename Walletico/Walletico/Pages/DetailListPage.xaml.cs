using ReactiveUI;
using Walletico.ViewModels;
using Xamarin.Forms.Xaml;

namespace Walletico.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailListPage : BaseContentPage<DetailListViewModel>
    {
        public DetailListPage()
        {
            InitializeComponent();
        }
    }
}