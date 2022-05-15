using DatabaseModel;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace EstateManager.MVVM.View
{
    /// <summary>
    /// Interaction logic for HistoryView.xaml
    /// </summary>
    public partial class HistoryView : UserControl
    {
        DatabaseEntitiesModel ctx = new DatabaseEntitiesModel();
        CollectionViewSource historyVSource;
        public HistoryView()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            historyVSource = (CollectionViewSource)this.FindResource("historyViewSource");
            historyVSource.Source = ctx.History.Local;
            ctx.History.Load();
        }
    }
}
