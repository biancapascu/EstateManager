using EstateManager.MVVM.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace EstateManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(string username)
        {
            InitializeComponent();

            DataRowCollection company = ConnectDB.GetDataTable("SELECT TOP 1 Name FROM Company").Rows;
            foreach (DataRow row in company)
            {
                companyName.Text = Convert.ToString(row.ItemArray[0]);
            }

            bool isAdmin = username == "admin" ? true : false;

            if (!isAdmin)
            {
                CatalogButton.Visibility = Visibility.Collapsed;
                RecordsButton.Visibility = Visibility.Collapsed;
                AccomodationButton.Visibility = Visibility.Collapsed;
                CompanyButton.Visibility = Visibility.Collapsed;
                UsersButton.Visibility = Visibility.Collapsed;
            }

            Application.Current.MainWindow = this;
        }
        private void Minimize(object sender, MouseButtonEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void Maximize(object sender, MouseButtonEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            else Application.Current.MainWindow.WindowState = WindowState.Normal;
        }

        private void Close(object sender, MouseButtonEventArgs e)
        {
            bool? Result = new CustomMessageBox("Are you sure you want to close the application? ", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();

            if (Result.Value)
            {
                Application.Current.Shutdown();
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            bool? Result = new CustomMessageBox("Are you sure you want to logout? ", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();

            if (Result.Value)
            {
                this.Hide();
                Window mainWindow = Application.Current.MainWindow;
                Window loginWindow = new Login();
                loginWindow.Show();
            }
        }
    }
}
