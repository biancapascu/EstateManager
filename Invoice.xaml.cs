using DatabaseModel;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EstateManager
{

    public partial class Invoice : Window
    {
        DataGrid recordDataGrid;
        DataGrid companyDataGrid;
        DatabaseEntitiesModel ctx = new DatabaseEntitiesModel();
        public Invoice(DataGrid recordDataGrid, DataGrid companyDataGrid)
        {
            InitializeComponent();
            this.recordDataGrid = recordDataGrid;
            this.companyDataGrid = companyDataGrid;

            Company companyItem = companyDataGrid.SelectedItem as Company;
            Records recordItem = recordDataGrid.SelectedItem as Records;
            List<Extras> extras = ctx.Extras.Where(extra => extra.IdRecord == recordItem.IdRecord).ToList();
            string accomodationName = recordItem.Accomodation;
            int accomodationPrice = ctx.Accomodation.First(item => item.Name == accomodationName).Price;
            DataTable dataTable = new DataTable();
            DataRow row = dataTable.NewRow();

            InvoiceName.Text = companyItem.Name;
            InvoiceCity.Text = companyItem.City+", "+ companyItem.Town;
            InvoicePostcode.Text = companyItem.Postcode;
            InvoiceSortCode.Text = companyItem.Swift;
            InvoiceAdress.Text = companyItem.Address;
            InvoiceAccountNo.Text = companyItem.Bank + " " + companyItem.Account;
            InvoiceEmail.Text = companyItem.Email;
            InvoiceWebsite.Text = companyItem.Website;
            InvoicePhone.Text = companyItem.Phone;
            InvoiceRegistration.Text = companyItem.Registration;
            InvoiceCustomerName.Text = recordItem.FirstName + " " + recordItem.LastName;
            InvoiceNumber.Text = "000" + recordItem.IdRecord.ToString();

            dataTable.Columns.Add("Description");
            dataTable.Columns.Add("Sum");
            dataTable.Columns.Add("Quantity");
            dataTable.Columns.Add("Subtotal");

            row["Description"] = accomodationName;
            row["Sum"] = accomodationPrice;
            row["Quantity"] = 1;
            row["Subtotal"] = accomodationPrice;
            dataTable.Rows.Add(row);

            int totalSum = accomodationPrice;

            foreach (Extras extra in extras)
            {
                int catalogPrice = ctx.Catalog.First(item => item.Name == extra.Name).Price;

                string description = extra.Name;
                int sum = catalogPrice;
                int quantity = extra.Quantity;
                int subtotal = sum * quantity;

                row = dataTable.NewRow();
                row["Description"] = description;
                row["Sum"] = sum;
                row["Quantity"] = quantity;
                row["Subtotal"] = subtotal;
                dataTable.Rows.Add(row);

                totalSum += subtotal;
            }
            InvoiceTotalBill.Text = totalSum.ToString()+" RON";
            InvoiceDataGrid.DataContext = dataTable.DefaultView;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.IsEnabled = false;
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(print, "invoice");
                }
            }
            finally
            {
                this.IsEnabled = true;
            }
        }
    }
}
