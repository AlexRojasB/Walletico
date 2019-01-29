using System;

namespace Walletico.Models
{
    public class Transaction : FreshMvvm.FreshBasePageModel
    {
        private bool isSelected;

        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime EntryDate { get; set; }
        public decimal Amount { get; set; }
        public bool IsSelected
        {
            get => isSelected; set
            {
                isSelected = value;
                RaisePropertyChanged();
            }
        }
    }
}
