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
using System.Windows.Shapes;

namespace EstateManager.MVVM.View
{
    /// <summary>
    /// Interaction logic for EditCompanyWindow.xaml
    /// </summary>
    public partial class EditCompanyWindow : Window
    {
        Company selectedRow;
        DatabaseEntitiesModel ctx;
        CollectionViewSource editCompanyVSource;
        public EditCompanyWindow(Company selectedRow, DatabaseEntitiesModel ctx)
        {
            InitializeComponent();

            this.selectedRow = selectedRow;
            this.ctx = ctx;

            editCompanyVSource = (CollectionViewSource)this.FindResource("companyViewSource");
            editCompanyVSource.Source = ctx.Company.Local;
            ctx.Company.Load();

            if (selectedRow == null)
            {
                grid1.DataContext = "";
            }
            else
            {
                nameTextBox.Text = selectedRow.Name;
                addressTextBox.Text = selectedRow.Address;
                townTextBox.Text = selectedRow.Town;
                cityTextBox.Text = selectedRow.City;
                postcodeTextBox.Text = selectedRow.Postcode;
                emailTextBox.Text = selectedRow.Email;
                phoneTextBox.Text = selectedRow.Phone;
                websiteTextBox.Text = selectedRow.Website;
                bankTextBox.Text = selectedRow.Bank;
                swiftTextBox.Text = selectedRow.Swift;
                accountTextBox.Text = selectedRow.Account;
                registrationTextBox.Text = selectedRow.Registration;

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            if(selectedRow == null)
            {

                Company company = new Company();

                company.Name = nameTextBox.Text;
                company.Address = addressTextBox.Text;
                company.Town = townTextBox.Text;
                company.City = cityTextBox.Text;
                company.Postcode = postcodeTextBox.Text;
                company.Email = emailTextBox.Text;
                company.Phone = phoneTextBox.Text;
                company.Website = websiteTextBox.Text;
                company.Bank = bankTextBox.Text;
                company.Swift = swiftTextBox.Text;
                company.Account = accountTextBox.Text;
                company.Registration = registrationTextBox.Text;
                
                ctx.Company.Add(company);

                ctx.SaveChanges();
                ctx.Company.Load();
                this.Close();
            }
            else
            {
                Company editedRow = ctx.Company.First(comp=> comp.Name == selectedRow.Name);

                editedRow.Name = nameTextBox.Text;
                editedRow.Address = addressTextBox.Text;
                editedRow.Town = townTextBox.Text;
                editedRow.City = cityTextBox.Text;
                editedRow.Postcode = postcodeTextBox.Text;
                editedRow.Email = emailTextBox.Text;
                editedRow.Phone = phoneTextBox.Text;
                editedRow.Website = websiteTextBox.Text;
                editedRow.Bank = bankTextBox.Text;
                editedRow.Swift = swiftTextBox.Text;
                editedRow.Account = accountTextBox.Text;
                editedRow.Registration = registrationTextBox.Text;

                ctx.SaveChanges();
                ctx.Company.Load();
                this.Close();
            }
        }
        
        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
