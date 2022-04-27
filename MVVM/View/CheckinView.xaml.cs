using DatabaseModel;
using System;
using System.Collections.Generic;
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
using System.Data.Entity;
using System.Data;

namespace EstateManager.MVVM.View
{
    /// <summary>
    /// Interaction logic for CheckinView.xaml
    /// </summary>
    public partial class CheckinView : UserControl
    {
        DatabaseEntitiesModel ctx = new DatabaseEntitiesModel();
        CollectionViewSource recordsVSource;
        public CheckinView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            recordsVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("recordsViewSource")));
            recordsVSource.Source = ctx.Records.Local;
            ctx.Records.Load();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Records record = null;
            try
            {
                record = new Records()
                {
                    StartTime = startTimeDatePicker.SelectedDate.Value,
                    EndTime = endTimeDatePicker.SelectedDate.Value,
                    Accomodation = accomodationTextBox.Text.Trim(),
                    FirstName = firstNameTextBox.Text.Trim(),
                    LastName = lastNameTextBox.Text.Trim(),
                    Phone = phoneTextBox.Text.Trim(),
                    Email = emailTextBox.Text.Trim(),
                    Adress = adressTextBox.Text.Trim(),
                    CNP = cNPTextBox.Text.Trim(),
                    Series = seriesTextBox.Text.Trim(),
                    Number = numberTextBox.Text.Trim()
                };
                ctx.Records.Add(record);
                recordsVSource.View.Refresh();
                ctx.SaveChanges();
            }
            catch (DataException ex)
            {
                new CustomMessageBox(ex.Message, MessageType.Error, MessageButtons.Ok);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            grid1.DataContext = "";
        }
    }
}
