using System.Windows.Controls;

namespace Carbon.UI
{
    /// <summary>
    /// Interaction logic for OptionsView.xaml
    /// </summary>
    public partial class OptionsView : UserControl
    {
        public OptionsView()
        {
            InitializeComponent();
            DataContext = new OptionsViewModel();
        }
    }
}
