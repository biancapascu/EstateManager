using DatabaseModel;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace EstateManager.MVVM.View
{
    /// <summary>
    /// Interaction logic for EditRecordWindow.xaml
    /// </summary>
    public partial class EditRecordWindow : Window
    {
        Records selectedRow;
        DatabaseEntitiesModel ctx;
        CollectionViewSource editRecordsVSource;

        public EditRecordWindow(Records selectedRow, DatabaseEntitiesModel ctx)
        {
            InitializeComponent();

            this.selectedRow = selectedRow;
            this.ctx = ctx;

            editRecordsVSource = (CollectionViewSource)this.FindResource("recordsViewSource");
            editRecordsVSource.Source = ctx.Records.Local;
            ctx.Records.Load();

            accomodationTextBox.Text = selectedRow.Accomodation;
            addressTextBox.Text = selectedRow.Address;
            cNPTextBox.Text = selectedRow.CNP;
            emailTextBox.Text = selectedRow.Email;
            firstNameTextBox.Text = selectedRow.FirstName;
            idRecordTextBox.Text = selectedRow.IdRecord.ToString();
            lastNameTextBox.Text = selectedRow.LastName;
            numberTextBox.Text = selectedRow.Number;
            phoneTextBox.Text = selectedRow.Phone;
            seriesTextBox.Text = selectedRow.Series;
            startTimeDatePicker.Text = selectedRow.StartTime.ToString();
            endTimeDatePicker.Text = selectedRow.EndTime.ToString();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            try
            {
                Records editedRow = ctx.Records.First(record => record.IdRecord == selectedRow.IdRecord);

                editedRow.Accomodation = accomodationTextBox.Text;
                editedRow.Address = addressTextBox.Text;
                editedRow.CNP = cNPTextBox.Text;
                editedRow.Email = emailTextBox.Text;
                editedRow.FirstName = firstNameTextBox.Text;
                editedRow.IdRecord = Convert.ToInt32(idRecordTextBox.Text);
                editedRow.LastName = lastNameTextBox.Text;
                editedRow.Number = numberTextBox.Text;
                editedRow.Phone = phoneTextBox.Text;
                editedRow.Series = seriesTextBox.Text;
                editedRow.StartTime = Convert.ToDateTime(startTimeDatePicker.Text);
                editedRow.EndTime = Convert.ToDateTime(endTimeDatePicker.Text);


                ctx.SaveChanges();
                ctx.Records.Load();
                this.Close();
            }
            catch (DataException ex)
            {
                new CustomMessageBox(ex.Message, MessageType.Error, MessageButtons.Ok);
            }
        }
    }
}
