using System.Windows.Controls;

namespace EstateManager.MVVM.View
{
    /// <summary>
    /// Interaction logic for ReserveView.xaml
    /// </summary>
    public partial class ReserveView : UserControl
    {

        public ReserveView()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
