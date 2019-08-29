using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Walletico.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTransactionPopup : ContentView
    {
        public delegate void ClickExpandDelegate();
        public AddTransactionPopup()
        {
            InitializeComponent();
        }

        private void IncomeFirstSectionTapped(object sender, EventArgs e)
        {

        }

        public ClickExpandDelegate OnExpandTapped { get; set; }
        public double IncomeFirstSectionHeigh => this.FirstSection.Height;
    }
}