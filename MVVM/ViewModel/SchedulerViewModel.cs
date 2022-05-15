using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Syncfusion.UI.Xaml.Scheduler;
using System.ComponentModel;
using DatabaseModel;
using EstateManager;

namespace WpfScheduler
{
   public class SchedulerViewModel : INotifyPropertyChanged
    {
        private ScheduleAppointmentCollection scheduleAppointmentCollection;
        public List<Reservations> Reservations { get; set; }

        public SchedulerViewModel()
        {
            try
            {
                ScheduleAppointmentCollection = new ScheduleAppointmentCollection();
                var dataTable = ConnectDB.GetDataTable("SELECT * FROM Reservations");
                Reservations = new List<Reservations>();
                Reservations = (from DataRow dr in dataTable.Rows
                                select new Reservations()
                                {
                                    Subject = dr["Subject"].ToString(),
                                    StartTime = dr["StartTime"] as DateTime? ?? DateTime.Now,
                                    EndTime = dr["EndTime"] as DateTime? ?? DateTime.Now,
                                    Location = dr["Location"].ToString(),
                                    Notes = dr["Notes"].ToString()
                                }).ToList();
                foreach (Reservations reservation in Reservations)
                {
                    ScheduleAppointmentCollection.Add(new ScheduleAppointment()
                        { 
                            Subject = reservation.Subject,
                            StartTime = reservation.StartTime,
                            EndTime = reservation.EndTime,
                            Location = reservation.Location,
                            Notes = reservation.Notes
                        });
                }
            }
            catch
            {
                new CustomMessageBox("Could not connect to database.", MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
        }
        public ScheduleAppointmentCollection ScheduleAppointmentCollection
        {
            get
            {
                return this.scheduleAppointmentCollection;
            }

            set
            {
                this.scheduleAppointmentCollection = value;
                this.RaiseOnPropertyChanged("ScheduleAppointmentCollection");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaiseOnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
