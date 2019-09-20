using AiForms.Dialogs.Abstractions;
using Xamarin.Forms.Xaml;

namespace Walletico.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationDialog : DialogView
    {
        public LocationDialog()
        {
            InitializeComponent();
        }

        public override void SetUp()
        {
            // called each opening dialog
        }


        public override void TearDown()
        {
            // called each closing dialog
        }

        public override void RunPresentationAnimation()
        {
            // define opening animation
        }

        public override void RunDismissalAnimation()
        {
            // define closing animation
        }

        public override void Destroy()
        {
            // define clean up process.
        }

        void Handle_OK_Clicked(object sender, System.EventArgs e)
        {
            // send complete notification to the dialog.
            DialogNotifier.Complete();
        }

        void Handle_Cancel_Clicked(object sender, System.EventArgs e)
        {
            // send cancel notification to the dialog.
            DialogNotifier.Cancel();
        }
    }
}