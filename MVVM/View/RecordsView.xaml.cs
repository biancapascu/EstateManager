using DatabaseModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EstateManager.MVVM.View
{
    /// <summary>
    /// Interaction logic for RecordsView.xaml
    /// </summary>
    public partial class RecordsView : UserControl
    {
        DatabaseEntitiesModel ctx = new DatabaseEntitiesModel();
        CollectionViewSource recordsVSource;
        Records record = new Records();

        DataTable dt = new DataTable();
        public RecordsView()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            recordsVSource = (CollectionViewSource)this.FindResource("recordsViewSource");
            recordsVSource.Source = ctx.Records.Local;
            ctx.Records.Load();
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if(recordsDataGrid.SelectedItem == null)
            {
                new CustomMessageBox("Select an existing item to edit!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
            }
            else 
            { 
                EditRecordWindow editRecordWindow = new EditRecordWindow(recordsDataGrid.SelectedItem as Records, ctx);
                editRecordWindow.Show();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (recordsDataGrid.SelectedItem == null)
            {
                new CustomMessageBox("Select an existing item to delete!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
            }
            else
            {
                record = (Records)recordsDataGrid.SelectedItem;
                ctx.Records.Remove(record);
                ctx.SaveChanges();
            }
        }
    }
}
