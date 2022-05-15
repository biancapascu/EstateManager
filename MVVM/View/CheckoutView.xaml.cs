using DatabaseModel;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace EstateManager.MVVM.View
{
    /// <summary>
    /// Interaction logic for CheckoutView.xaml
    /// </summary>
    public partial class CheckoutView : UserControl
    {
        DatabaseEntitiesModel ctx = new DatabaseEntitiesModel();
        CollectionViewSource recordsVSource;
        CollectionViewSource companyVSource;

        public CheckoutView()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            recordsVSource = (CollectionViewSource)this.FindResource("recordsViewSource");
            recordsVSource.Source = ctx.Records.Local;
            ctx.Records.Load();
            companyVSource = (CollectionViewSource)this.FindResource("companyViewSource");
            companyVSource.Source = ctx.Company.Local;
            ctx.Company.Load();
        }

        private void btnPreview_Click(object sender, RoutedEventArgs e)
        {
            Invoice invoiceWindow = new Invoice(recordsDataGrid,companyDataGrid);
            invoiceWindow.Show();
        }
    }
}
