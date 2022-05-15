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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EstateManager.MVVM.View
{
    /// <summary>
    /// Interaction logic for AccomodationView.xaml
    /// </summary>
    public partial class AccomodationView : UserControl
    {
        DatabaseEntitiesModel ctx = new DatabaseEntitiesModel();
        CollectionViewSource accomodationVSource;
        Accomodation accomodation = null;
        public AccomodationView()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            accomodationVSource = (CollectionViewSource)this.FindResource("accomodationViewSource");
            accomodationVSource.Source = ctx.Accomodation.Local;
            ctx.Accomodation.Load();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                accomodation = new Accomodation()
                {
                    Name = nameTextBox.Text.Trim(),
                    Type = typeTextBox.Text.Trim(),
                    Price = int.Parse(priceTextBox.Text.Trim())
                };
                ctx.Accomodation.Add(accomodation);
                accomodationVSource.View.Refresh();
                ctx.SaveChanges();
                new CustomMessageBox("Item added successfully!", MessageType.Confirmation, MessageButtons.Ok).ShowDialog();
            }
            catch (DataException ex)
            {
                new CustomMessageBox(ex.Message, MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
            finally
            {
                accomodation = null;
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (accomodationDataGrid.SelectedItem == null)
                    new CustomMessageBox("Select an existing item to edit!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                else
                {
                    accomodation = (Accomodation)accomodationDataGrid.SelectedItem;
                    accomodation.Name = nameTextBox.Text.Trim();
                    accomodation.Price = int.Parse(priceTextBox.Text.Trim());
                    accomodation.Type = nameTextBox.Text.Trim();
                    ctx.SaveChanges();
                }
            }
            catch (DataException ex)
            {
                new CustomMessageBox(ex.Message, MessageType.Error, MessageButtons.Ok);
            }
            finally
            {
                accomodation = null;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (accomodationDataGrid.SelectedItem == null)
                    new CustomMessageBox("Select an existing item to delete!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                else
                {
                    accomodation = (Accomodation)accomodationDataGrid.SelectedItem;
                    ctx.Accomodation.Remove(accomodation);
                    ctx.SaveChanges();
                }
            }
            catch (DataException ex)
            {
                new CustomMessageBox(ex.Message, MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
            finally
            {
                accomodation = null;
            }
        }
    }
}
