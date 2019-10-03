using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Walletico.Models;
using Walletico.Shared;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace Walletico.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuickEntryPopup : ContentView
    {
        public delegate void ClickExpandDelegate();
        private readonly StringBuilder sbAmount;
        public static readonly BindableProperty TotalAmountProperty = BindableProperty.Create(nameof(TotalAmount), typeof(decimal), typeof(QuickEntryPopup), decimal.Zero);
        public static readonly BindableProperty EnableLocationProperty = BindableProperty.Create(nameof(EnableLocation), typeof(bool), typeof(QuickEntryPopup), default(bool), BindingMode.TwoWay);
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(QuickEntryPopup), null);
        public static readonly BindableProperty PlacesProperty = BindableProperty.Create(nameof(Places), typeof(IEnumerable<Geoplace>), typeof(QuickEntryPopup), Enumerable.Empty<Geoplace>(), BindingMode.TwoWay);
        public static readonly BindableProperty StartQuickEntryCategoryProperty = BindableProperty.Create(nameof(StartQuickEntryCategory), typeof(Category), typeof(QuickEntryPopup), default(string), BindingMode.TwoWay);
        public static readonly BindableProperty CenterQuickEntryCategoryProperty = BindableProperty.Create(nameof(CenterQuickEntryCategory), typeof(Category), typeof(QuickEntryPopup), default(string), BindingMode.TwoWay);
        public static readonly BindableProperty EndQuickEntryCategoryProperty = BindableProperty.Create(nameof(EndQuickEntryCategory), typeof(Category), typeof(QuickEntryPopup), default(string), BindingMode.TwoWay);

        public QuickEntryPopup()
        {
            InitializeComponent();
            this.sbAmount = new StringBuilder();
        }

        private void IncomeFirstSectionTapped(object sender, EventArgs e)
        {
            OnExpandTapped?.Invoke();
        }

        private async void QuickEntryItemTapped(object sender, EventArgs e)
        {
            var quickControl = (sender as PancakeView);
            if (quickControl is null) return;
            var quickCategory = (quickControl.BindingContext as Category);
            if (quickCategory is null) return;

            if (!quickCategory.IsSelected)
            {
                await this.UnselectAllCategories();
                quickCategory.IsSelected = true;
                await quickControl.ColorTo(Color.FromHex("#FFF"), (Color)Application.Current.Resources["PrimaryLight"], c => quickControl.BackgroundGradientEndColor = c, 200, Easing.SinIn);
            }
        }


        private async Task UnselectAllCategories()
        {
            if (this.StartQuickEntryCategory.IsSelected)
            {
                await this.StartQuickEntry.ColorTo((Color)Application.Current.Resources["PrimaryLight"], Color.FromHex("#FFF"), c => this.StartQuickEntry.BackgroundGradientEndColor = c, 200, Easing.SinIn);
            }
            if (this.CenterQuickEntryCategory.IsSelected)
            {
                await this.CenterQuickEntry.ColorTo((Color)Application.Current.Resources["PrimaryLight"], Color.FromHex("#FFF"), c => this.CenterQuickEntry.BackgroundGradientEndColor = c, 200, Easing.SinIn);

            }
            if (this.EndQuickEntryCategory.IsSelected)
            {
                await this.EndQuickEntry.ColorTo((Color)Application.Current.Resources["PrimaryLight"], Color.FromHex("#FFF"), c => this.EndQuickEntry.BackgroundGradientEndColor = c, 200, Easing.SinIn);
            }
            this.StartQuickEntryCategory.IsSelected = false;
            this.CenterQuickEntryCategory.IsSelected = false;
            this.EndQuickEntryCategory.IsSelected = false;
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

        public decimal TotalAmount { get => (decimal)GetValue(TotalAmountProperty); set => SetValue(TotalAmountProperty, value); }
        public bool EnableLocation
        {
            get => (bool)GetValue(EnableLocationProperty);
            set => SetValue(EnableLocationProperty, value);
        }
        public ICommand Command { get => (ICommand)GetValue(CommandProperty); set => SetValue(CommandProperty, value); }
        public IEnumerable<Geoplace> Places { get => (IEnumerable<Geoplace>)GetValue(PlacesProperty); set => SetValue(PlacesProperty, value); }


        public Category StartQuickEntryCategory { get => (Category)GetValue(StartQuickEntryCategoryProperty); set => SetValue(StartQuickEntryCategoryProperty, value); }
        public Category CenterQuickEntryCategory { get => (Category)GetValue(CenterQuickEntryCategoryProperty); set => SetValue(CenterQuickEntryCategoryProperty, value); }
        public Category EndQuickEntryCategory { get => (Category)GetValue(EndQuickEntryCategoryProperty); set => SetValue(EndQuickEntryCategoryProperty, value); }


    }
}