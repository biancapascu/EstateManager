using DatabaseModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
        CollectionViewSource accomodationVSource;
        Records record = null;
        public CheckinView()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            recordsVSource = (CollectionViewSource)this.FindResource("recordsViewSource");
            recordsVSource.Source = ctx.Records.Local;
            ctx.Records.Load();
            accomodationVSource = (CollectionViewSource)this.FindResource("accomodationViewSource");
            accomodationVSource.Source = ctx.Accomodation.Local;
            ctx.Accomodation.Load();
            accomodationTextBox.ItemsSource = ctx.Accomodation.Local;
            accomodationTextBox.DisplayMemberPath = "Name";
            accomodationTextBox.SelectedValuePath = "Name";
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
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
                    Address = addressTextBox.Text.Trim(),
                    CNP = cNPTextBox.Text.Trim(),
                    Series = seriesTextBox.Text.Trim(),
                    Number = numberTextBox.Text.Trim()
                };
                ctx.Records.Add(record);
                recordsVSource.View.Refresh();
                ctx.SaveChanges();
                new CustomMessageBox("Record added successfully!", MessageType.Confirmation, MessageButtons.Ok).ShowDialog();
            }
            catch (DataException ex)
            {
                new CustomMessageBox(ex.Message, MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
            finally
            {
                record = null;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            grid1.DataContext = "";
        }
    }
}
