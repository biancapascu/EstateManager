using DatabaseModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data.Entity;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

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
            if(Validate(grid1))
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
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            grid1.DataContext = "";
        }

        private bool Validate(object sender)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            List<TextBox> gridlist = new List<TextBox>();
            foreach(TextBox tb in gridlist)
            {
                if(tb.Name != idRecordTextBox.Name)
                {
                    gridlist.Add(tb);
                }
            }
            if (gridlist.Any(tb => string.IsNullOrEmpty(tb.Text)))
            {
                new CustomMessageBox("All fields must be completed!",MessageType.Warning,MessageButtons.Ok).ShowDialog();
                return false;
            }
            if (regex.IsMatch(emailTextBox.Text) == false)
            {
                new CustomMessageBox("Email entered wrong!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return false;
            }
            if (startTimeDatePicker.SelectedDate.Value > endTimeDatePicker.SelectedDate.Value)
            {
                new CustomMessageBox("Start date must be before End date!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return false;
            }
            if (cNPTextBox.Text.Length != 13)
            {
                new CustomMessageBox("CNP entered wrong!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return false;
            }
            if (numberTextBox.Text.Length != 6)
            {
                new CustomMessageBox("Number entered wrong!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return false;
            }
            if (seriesTextBox.Text.Length != 2)
            {
                new CustomMessageBox("Series entered wrong!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return false;
            }
            return true;
        }
    }
}
