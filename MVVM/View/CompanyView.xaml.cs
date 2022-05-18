using DatabaseModel;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for CompanyView.xaml
    /// </summary>
    public partial class CompanyView : UserControl
    {
        DatabaseEntitiesModel ctx = new DatabaseEntitiesModel();
        CollectionViewSource companyVSource;
        Company company = new Company();
        public CompanyView()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            companyVSource = (CollectionViewSource)this.FindResource("companyViewSource");
            companyVSource.Source = ctx.Company.Local;
            ctx.Company.Load();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            company = null;
            EditCompanyWindow editCompanyWindow = new EditCompanyWindow(company, ctx);
            editCompanyWindow.Show();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (companyDataGrid.SelectedItem == null)
            {
                new CustomMessageBox("Select an existing item to edit!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
            }
            else
            {
                EditCompanyWindow editCompanyWindow = new EditCompanyWindow(companyDataGrid.SelectedItem as Company, ctx);
                editCompanyWindow.Show();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (companyDataGrid.SelectedItem == null)
            {
                new CustomMessageBox("Select an existing item to delete!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
            }
            else
            {
                company = (Company)companyDataGrid.SelectedItem;
                ctx.Company.Remove(company);
                ctx.SaveChanges();
            }
        }
    }
}
