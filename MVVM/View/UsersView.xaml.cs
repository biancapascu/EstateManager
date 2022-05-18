using DatabaseModel;
using System.Data;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace EstateManager.MVVM.View
{
    /// <summary>
    /// Interaction logic for UsersView.xaml
    /// </summary>
    public partial class UsersView : UserControl
    {
        DatabaseEntitiesModel ctx = new DatabaseEntitiesModel();
        CollectionViewSource usersVSource;
        Users user = null;
        public UsersView()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            usersVSource = (CollectionViewSource)this.FindResource("usersViewSource");
            usersVSource.Source = ctx.Users.Local;
            ctx.Users.Load();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                user = new Users()
                {
                    Username = usernameTextBox.Text.Trim(),
                    Password = passwordTextBox.Text.Trim()
                };
                ctx.Users.Add(user);
                usersVSource.View.Refresh();
                ctx.SaveChanges();
                new CustomMessageBox("User added successfully!", MessageType.Confirmation, MessageButtons.Ok).ShowDialog();
            }
            catch (DataException ex)
            {
                new CustomMessageBox(ex.Message, MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
            finally
            {
                user = null;
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (usersDataGrid.SelectedItem == null)
                    new CustomMessageBox("Select an existing user to edit!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                else
                {
                    user = (Users)usersDataGrid.SelectedItem;
                    user.Username = usernameTextBox.Text.Trim();
                    user.Password = passwordTextBox.Text.Trim();
                    ctx.SaveChanges();
                }
            }
            catch (DataException ex)
            {
                new CustomMessageBox(ex.Message, MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
            finally
            {
                user = null;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (usersDataGrid.SelectedItem == null)
                    new CustomMessageBox("Select an existing user to delete!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                else
                {
                    user = (Users)usersDataGrid.SelectedItem;
                    ctx.Users.Remove(user);
                    ctx.SaveChanges();
                }
            }
            catch (DataException ex)
            {
                new CustomMessageBox(ex.Message, MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
            finally
            {
                user = null;
            }
        }

        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            UserHistoryWindow userHistory = new UserHistoryWindow();
            userHistory.Show();
        }
    }
}
