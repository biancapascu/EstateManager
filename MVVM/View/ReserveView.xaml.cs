﻿using System;
using System.Collections.Generic;
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
using Syncfusion.SfSkinManager;
using Syncfusion.UI.Xaml.Scheduler;

namespace EstateManager.MVVM.View
{
    /// <summary>
    /// Interaction logic for ReserveView.xaml
    /// </summary>
    public partial class ReserveView : UserControl
    {

        public ReserveView()
        {
            InitializeComponent();
            this.scheduler.AppointmentEditorClosing += OnSchedulerAppointmentEditorClosing;
            this.scheduler.AppointmentEditorOpening += OnSchedulerAppointmentEditorOpening;
        }
        private void OnSchedulerAppointmentEditorOpening(object sender, AppointmentEditorOpeningEventArgs e)
        {
            e.AppointmentEditorOptions = AppointmentEditorOptions.All | (~AppointmentEditorOptions.Background & ~AppointmentEditorOptions.Foreground & ~AppointmentEditorOptions.Reminder & ~AppointmentEditorOptions.Resource);
        }
        private void OnSchedulerAppointmentEditorClosing(object sender, AppointmentEditorClosingEventArgs e)
        {
            if (e.Action == AppointmentEditorAction.Add)
            {
                string sqlAdd = "INSERT INTO Reservations ([Subject],[StartTime],[EndTime]) VALUES('" + e.Appointment.Subject + "', '" + e.Appointment.StartTime.ToString("yyyy-MM-dd HH:mm:ss") + "' , '" + e.Appointment.EndTime.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                ConnectDB.ExecuteSQLQuery(sqlAdd);
            }
            else if (e.Action == AppointmentEditorAction.Delete)
            {
                string sqlDelete = "DELETE from Reservations where Subject ='" + e.Appointment.Subject + "';";
                ConnectDB.ExecuteSQLQuery(sqlDelete);
            }
            else if (e.Action == AppointmentEditorAction.Edit)
            {
                string sqlUpdate = "UPDATE Reservations set StartTime='" + e.Appointment.StartTime + "',EndTime='" + e.Appointment.EndTime + "' where Subject='" + e.Appointment.Subject + "';";
                ConnectDB.ExecuteSQLQuery(sqlUpdate);
            }
        }
    }
}
