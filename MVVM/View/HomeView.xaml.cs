using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Windows.Controls;
using System.Windows.Data;
using DatabaseModel;
using LiveCharts;
using LiveCharts.Wpf;
namespace EstateManager.MVVM.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
            DataContext = this;
        }

    }
}
