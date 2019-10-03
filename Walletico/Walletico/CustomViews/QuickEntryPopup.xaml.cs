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
        private const uint colorAnimationSpeed = 200;
        private readonly StringBuilder sbAmount;
        private readonly Color quickEntryTappedColor;
        private readonly Color quickEntryUnTappedColor;
        public static readonly BindableProperty TotalAmountProperty = BindableProperty.Create(nameof(TotalAmount), typeof(decimal), typeof(QuickEntryPopup), decimal.Zero);
        public static readonly BindableProperty EnableLocationProperty = BindableProperty.Create(nameof(EnableLocation), typeof(bool), typeof(QuickEntryPopup), default(bool), BindingMode.TwoWay);
        public static readonly BindableProperty GPSCommandProperty = BindableProperty.Create(nameof(GPSCommand), typeof(ICommand), typeof(QuickEntryPopup), null);
        public static readonly BindableProperty PlacesProperty = BindableProperty.Create(nameof(Places), typeof(IEnumerable<Geoplace>), typeof(QuickEntryPopup), Enumerable.Empty<Geoplace>(), BindingMode.TwoWay);
        public static readonly BindableProperty StartQuickEntryCategoryProperty = BindableProperty.Create(nameof(StartQuickEntryCategory), typeof(Category), typeof(QuickEntryPopup), default(string), BindingMode.TwoWay);
        public static readonly BindableProperty CenterQuickEntryCategoryProperty = BindableProperty.Create(nameof(CenterQuickEntryCategory), typeof(Category), typeof(QuickEntryPopup), default(string), BindingMode.TwoWay);
        public static readonly BindableProperty EndQuickEntryCategoryProperty = BindableProperty.Create(nameof(EndQuickEntryCategory), typeof(Category), typeof(QuickEntryPopup), default(string), BindingMode.TwoWay);

        public QuickEntryPopup()
        {
            InitializeComponent();
            this.sbAmount = new StringBuilder();
            this.quickEntryTappedColor = (Color)Application.Current.Resources["PrimaryLight"];
            this.quickEntryUnTappedColor = Color.White;
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
                var selectTask = quickControl.ColorTo(this.quickEntryUnTappedColor, this.quickEntryTappedColor, c => quickControl.BackgroundGradientEndColor = c, colorAnimationSpeed, Easing.SinIn);
                await Task.WhenAll(this.UnselectAllCategories(), selectTask);
                quickCategory.IsSelected = true;
            }
        }


        private Task<bool> UnselectAllCategories()
        {
            if (this.StartQuickEntryCategory.IsSelected)
            {
                this.StartQuickEntryCategory.IsSelected = false;
                return this.StartQuickEntry.ColorTo(this.quickEntryTappedColor, this.quickEntryUnTappedColor, c => this.StartQuickEntry.BackgroundGradientEndColor = c, colorAnimationSpeed, Easing.SinIn);
            }
            else if (this.CenterQuickEntryCategory.IsSelected)
            {
                this.CenterQuickEntryCategory.IsSelected = false;
                return this.CenterQuickEntry.ColorTo(this.quickEntryTappedColor, this.quickEntryUnTappedColor, c => this.CenterQuickEntry.BackgroundGradientEndColor = c, colorAnimationSpeed, Easing.SinIn);

            }
            else if (this.EndQuickEntryCategory.IsSelected)
            {
                this.EndQuickEntryCategory.IsSelected = false;
                return this.EndQuickEntry.ColorTo(this.quickEntryTappedColor, this.quickEntryUnTappedColor, c => this.EndQuickEntry.BackgroundGradientEndColor = c, colorAnimationSpeed, Easing.SinIn);
            }
            return Task.FromResult(false);
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
        public ICommand GPSCommand { get => (ICommand)GetValue(GPSCommandProperty); set => SetValue(GPSCommandProperty, value); }
        public IEnumerable<Geoplace> Places { get => (IEnumerable<Geoplace>)GetValue(PlacesProperty); set => SetValue(PlacesProperty, value); }


        public Category StartQuickEntryCategory { get => (Category)GetValue(StartQuickEntryCategoryProperty); set => SetValue(StartQuickEntryCategoryProperty, value); }
        public Category CenterQuickEntryCategory { get => (Category)GetValue(CenterQuickEntryCategoryProperty); set => SetValue(CenterQuickEntryCategoryProperty, value); }
        public Category EndQuickEntryCategory { get => (Category)GetValue(EndQuickEntryCategoryProperty); set => SetValue(EndQuickEntryCategoryProperty, value); }


    }
}