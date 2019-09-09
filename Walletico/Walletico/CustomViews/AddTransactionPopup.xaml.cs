using System;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Walletico.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTransactionPopup : ContentView
    {
        public delegate void ClickExpandDelegate();
        private StringBuilder sbAmount;
        public AddTransactionPopup()
        {
            InitializeComponent();
            this.sbAmount = new StringBuilder();
        }

        private void IncomeFirstSectionTapped(object sender, EventArgs e)
        {
            OnExpandTapped?.Invoke();
        }

        private void NumberButtonEvent(object sender, EventArgs e)
        {
            this.UpdateAmount((sender as Button).Text);
        }

        private void DelButtonEvent(object sender, EventArgs e)
        {
            if (this.sbAmount.Length > 0)
            {
                this.sbAmount.Remove(this.sbAmount.Length - 1, 1);
                this.Amount.Text = sbAmount.ToString();
            }
        }

        private void UpdateAmount(string digit)
        {
            this.sbAmount.Append(digit);
            this.Amount.Text = sbAmount.ToString();
        }

        public ClickExpandDelegate OnExpandTapped { get; set; }
        public double IncomeFirstSectionHeigh => this.FirstSection.Height + this.EntrySection.Height;
    }
}