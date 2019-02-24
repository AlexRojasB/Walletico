namespace Walletico.Models
{
    public class SelectableModelBase : FreshMvvm.FreshBasePageModel
    {
        private bool isSelected;

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
