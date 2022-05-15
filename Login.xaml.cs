using System;
using System.Data.SqlClient;
using System.Windows;
using System.Data;

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
                if (count == 1)
                {
                    String queryAdd = "INSERT INTO History (Username,DateTime) VALUES('" + txtUsername.Text + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    SqlCommand sqlCmdAdd = new SqlCommand(queryAdd, sqlCon);
                    sqlCmdAdd.ExecuteNonQuery();
                    MainWindow dashboard = new MainWindow(txtUsername.Text);
                    dashboard.Show();
                    this.Close();
                }
                else
                {
                    new CustomMessageBox("Username or password is incorrect.", MessageType.Error, MessageButtons.Ok).ShowDialog();
                }

            }
            catch (Exception ex)
            {
                new CustomMessageBox(ex.Message, MessageType.Warning, MessageButtons.Ok).ShowDialog();
            }
            finally
            {
                sqlCon.Close();
            }
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
