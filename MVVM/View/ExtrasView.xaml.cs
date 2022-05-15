using DatabaseModel;
using System;
using System.Data;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


namespace EstateManager.MVVM.View
{
    public partial class ExtrasView : UserControl
    {
        DatabaseEntitiesModel ctx = new DatabaseEntitiesModel();
        CollectionViewSource extrasVSource;
        CollectionViewSource catalogVSource;
        CollectionViewSource recordsVSource;
        Extras extra = null;
        public ExtrasView()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            extrasVSource = (CollectionViewSource)this.FindResource("extrasViewSource");
            extrasVSource.Source = ctx.Extras.Local;
            ctx.Extras.Load();

            catalogVSource = (CollectionViewSource)this.FindResource("catalogViewSource");
            catalogVSource.Source = ctx.Catalog.Local;
            ctx.Catalog.Load();
            nameTextBox.ItemsSource = ctx.Catalog.Local;
            nameTextBox.DisplayMemberPath = "Name";
            nameTextBox.SelectedValuePath = "Name";

            recordsVSource = (CollectionViewSource)this.FindResource("recordsViewSource");
            recordsVSource.Source = ctx.Records.Local;
            ctx.Records.Load();
            idRecordTextBox.ItemsSource = ctx.Records.Local;
            idRecordTextBox.DisplayMemberPath = "IdRecord";
            idRecordTextBox.SelectedValuePath = "IdRecord";

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                extra = new Extras()
                {
                    IdRecord = Convert.ToInt32(idRecordTextBox.Text),
                    Name = nameTextBox.Text.Trim(),
                    Quantity = Convert.ToInt32(quantityTextBox.Text)
                };
                ctx.Extras.Add(extra);
                extrasVSource.View.Refresh();
                ctx.SaveChanges();
                new CustomMessageBox("Service added successfully!", MessageType.Confirmation, MessageButtons.Ok).ShowDialog();
            }
            catch (DataException ex)
            {
                new CustomMessageBox(ex.Message, MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
            finally
            {
                extra = null;
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (extrasDataGrid.SelectedItem == null)
                    new CustomMessageBox("Select an existing service to edit!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                else
                {
                    extra = (Extras)extrasDataGrid.SelectedItem;
                    extra.IdExtra = Convert.ToInt32(idRecordTextBox.Text);
                    extra.Name = nameTextBox.Text.Trim();
                    extra.Quantity = Convert.ToInt32(quantityTextBox.Text);
                    ctx.SaveChanges();
                }
            }
            catch (DataException ex)
            {
                new CustomMessageBox(ex.Message, MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
            finally
            {
                extra = null;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (extrasDataGrid.SelectedItem == null)
                    new CustomMessageBox("Select an existing service to delete!", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                else
                {
                    extra = (Extras)extrasDataGrid.SelectedItem;
                    ctx.Extras.Remove(extra);
                    ctx.SaveChanges();
                }
            }
            catch (DataException ex)
            {
                new CustomMessageBox(ex.Message, MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
            finally
            {
                extra = null;
            }
        }
    }
}
