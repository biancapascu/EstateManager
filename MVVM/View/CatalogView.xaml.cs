using DatabaseModel;
using System.Data;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace EstateManager.MVVM.View
{
    /// <summary>
    /// Interaction logic for CatalogView.xaml
    /// </summary>
    public partial class CatalogView : UserControl
    {
        DatabaseEntitiesModel ctx = new DatabaseEntitiesModel();
        CollectionViewSource catalogVSource;
        Catalog catalog = null;
        public CatalogView()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            catalogVSource = (CollectionViewSource)this.FindResource("catalogViewSource");
            catalogVSource.Source = ctx.Catalog.Local;
            ctx.Catalog.Load();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                catalog = new Catalog()
                {
                    Name = nameTextBox.Text.Trim(),
                    Price = int.Parse(priceTextBox.Text.Trim())
                };
                ctx.Catalog.Add(catalog);
                catalogVSource.View.Refresh();
                ctx.SaveChanges();
                new CustomMessageBox("Item added successfully!", MessageType.Confirmation, MessageButtons.Ok).ShowDialog();
            }
            catch (DataException ex)
            {
                new CustomMessageBox(ex.Message, MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
            finally
            {
                catalog = null;
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (catalogDataGrid.SelectedItem == null)
                    new CustomMessageBox("Select an existing item to edit!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                else
                {
                    catalog = (Catalog)catalogDataGrid.SelectedItem;
                    catalog.Name = nameTextBox.Text.Trim();
                    catalog.Price = int.Parse(priceTextBox.Text.Trim());
                    ctx.SaveChanges();
                }
            }
            catch (DataException ex)
            {
                new CustomMessageBox(ex.Message, MessageType.Error, MessageButtons.Ok);
            }
            finally
            {
                catalog = null;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (catalogDataGrid.SelectedItem == null)
                    new CustomMessageBox("Select an existing item to delete!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                else
                {
                    catalog = (Catalog)catalogDataGrid.SelectedItem;
                    ctx.Catalog.Remove(catalog);
                    ctx.SaveChanges();
                }
            }
            catch (DataException ex)
            {
                new CustomMessageBox(ex.Message, MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
            finally
            {
                catalog = null;
            }
        }
    }
}
