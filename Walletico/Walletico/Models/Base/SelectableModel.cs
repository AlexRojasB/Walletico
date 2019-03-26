namespace Walletico.Models.Base
{
    public class SelectableModel : BaseModel, ISelectable
    {
        private bool _isSelected;

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    this.NotifyPropertyChanged();
                }
            }
        }
    }
}
