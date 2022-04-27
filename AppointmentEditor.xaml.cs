﻿using Syncfusion.UI.Xaml.Scheduler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace EstateManager
{
    /// <summary>
    /// Interaction logic for AppointmentEditor.xaml
    /// </summary>
    public partial class AppointmentEditor : Window
    {
        private SfScheduler scheduler;
        private ScheduleAppointment appointment;
        public AppointmentEditor(SfScheduler scheduler, ScheduleAppointment appointment, DateTime dateTime)
        {
            InitializeComponent();
            this.scheduler = scheduler;
            this.appointment = appointment;
            if (appointment != null)
            {
                this.Subject.Text = appointment.Subject;
                this.StartDatePicker.Value = appointment.StartTime.Date;
                this.EndDatePicker.Value = appointment.EndTime.Date;
                this.StartTimePicker.Value = appointment.StartTime;
                this.EndTimePicker.Value = appointment.EndTime;
                this.location.Text = appointment.Location;
                this.description.Text = appointment.Notes;
            }
            else
            {
                this.StartDatePicker.Value = dateTime.Date;
                this.EndDatePicker.Value = dateTime.Date;
                this.StartTimePicker.Value = dateTime;
                this.EndTimePicker.Value = dateTime.AddHours(1);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (appointment == null)
            {
                var scheduleAppointment = new ScheduleAppointment();
                scheduleAppointment.Subject = this.Subject.Text;
                scheduleAppointment.StartTime = this.StartDatePicker.Value.Value.Date.Add(this.StartTimePicker.Value.Value.TimeOfDay);
                scheduleAppointment.EndTime = this.EndDatePicker.Value.Value.Date.Add(this.EndTimePicker.Value.Value.TimeOfDay);
                scheduleAppointment.Location = this.location.Text;
                scheduleAppointment.Notes = this.description.Text;

                this.scheduler.ItemsSource = new ScheduleAppointmentCollection();
                string sqlAdd = "INSERT INTO Reservations ([Subject],[StartTime],[EndTime],[Location],[Notes]) VALUES('" + scheduleAppointment.Subject + "', '" + scheduleAppointment.StartTime.ToString("yyyy-MM-dd HH:mm:ss") + "' , '" + scheduleAppointment.EndTime.ToString("yyyy-MM-dd HH:mm:ss") + "' , '" + scheduleAppointment.Location + "' , '" + scheduleAppointment.Notes + "')";
                ConnectDB.ExecuteSQLQuery(sqlAdd);
                
                (this.scheduler.ItemsSource as ScheduleAppointmentCollection).Add(scheduleAppointment);
            }
            else
            {
                appointment.Subject = this.Subject.Text;
                appointment.StartTime = this.StartDatePicker.Value.Value.Date.Add(this.StartTimePicker.Value.Value.TimeOfDay);
                appointment.EndTime = this.EndDatePicker.Value.Value.Date.Add(this.EndTimePicker.Value.Value.TimeOfDay);
                appointment.Location = this.location.Text;
                appointment.Notes = this.description.Text;
                string sqlUpdate = "UPDATE Reservations set StartTime='" + appointment.StartTime.ToString("yyyy-MM-dd HH:mm:ss") + "',EndTime='" + appointment.EndTime.ToString("yyyy-MM-dd HH:mm:ss") + "',Location='" + appointment.Location + "',Notes='" + appointment.Notes + "' where Subject='" + appointment.Subject + "';";
                ConnectDB.ExecuteSQLQuery(sqlUpdate);
            }
            this.Close();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (appointment != null)
            {
                (this.scheduler.ItemsSource as ScheduleAppointmentCollection).Remove(appointment);
                string sqlDelete = "DELETE from Reservations where Subject ='" + appointment.Subject + "';";
                ConnectDB.ExecuteSQLQuery(sqlDelete);
            }
            this.Close();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            this.Save.Click -= this.Save_Click;
            this.Cancel.Click -= this.Cancel_Click;
            this.scheduler = null;
            this.appointment = null;
        }
    }
}
