﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
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
using System.Data;
using DatabaseModel;

namespace EstateManager
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection("data source=(LocalDB)\\MSSQLLocalDB;attachdbfilename=C:\\Users\\bianca\\Desktop\\AplicatieLicenta\\EstateManager\\Database.mdf;integrated security=True");
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                String query = "SELECT COUNT(1) FROM Users WHERE Username=@Username AND Password=@Password";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                sqlCmd.Parameters.AddWithValue("@Password", txtPassword.Password);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                if(count == 1)
                {
                    MainWindow dashboard = new MainWindow();
                    dashboard.Show();
                    this.Close();
                }
                else
                {
                    new CustomMessageBox("Username or password is incorrect.", MessageType.Error, MessageButtons.Ok).ShowDialog();
                }

            }
            catch(Exception ex)
            {
                new CustomMessageBox(ex.Message, MessageType.Warning, MessageButtons.Ok).ShowDialog();
            }
            finally
            {
                sqlCon.Close();
            }
        }
    }
}
