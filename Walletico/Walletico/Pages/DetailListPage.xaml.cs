using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Walletico.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailListPage : ContentPage
    {
        public DetailListPage()
        {
            InitializeComponent();
        }

        private void CollectionView_DescendantAdded(object sender, ElementEventArgs e)
        {

        }
    }
}