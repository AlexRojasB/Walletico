using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Walletico.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTransactionPopup : ContentView
    {
        public delegate void ClickExpandDelegate();
        private readonly StringBuilder sbAmount;

        public static readonly BindableProperty TotalAmountProperty = BindableProperty.Create(nameof(TotalAmount), typeof(decimal), typeof(AddTransactionPopup), decimal.Zero);
        public static readonly BindableProperty EnableLocationProperty = BindableProperty.Create(nameof(EnableLocation), typeof(bool), typeof(AddTransactionPopup), default(bool), BindingMode.TwoWay);
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(AddTransactionPopup), null);

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
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName.Equals(EnableLocationProperty.PropertyName))
            {
                base.OnPropertyChanged(propertyName);
            }
        }

        private void UpdateAmount(string digit)
        {
            this.sbAmount.Append(digit);
            this.Amount.Text = sbAmount.ToString();
        }

        public ClickExpandDelegate OnExpandTapped { get; set; }
        public double IncomeFirstSectionHeigh => this.FirstSection.Height + this.EntrySection.Height;

        public decimal TotalAmount { get => (decimal)GetValue(TotalAmountProperty); set => SetValue(TotalAmountProperty, value); }
        public bool EnableLocation
        {
            get => (bool)GetValue(EnableLocationProperty); 
            set {
                SetValue(EnableLocationProperty, value);
                
            }
        }
        public ICommand Command { get => (ICommand)GetValue(CommandProperty); set => SetValue(CommandProperty, value); }
    }
}