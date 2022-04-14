using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseModel;

namespace EstateManager.MVVM.ViewModel
{
    class ReserveViewModel
    {
        public List<Reservations> Reservations { get; set; }
        public ReserveViewModel()
        {
            try
            {
                var dataTable = ConnectDB.GetDataTable("SELECT * FROM Reservations");
                Reservations = new List<Reservations>();
                Reservations = (from DataRow dr in dataTable.Rows
                            select new Reservations()
                            {
                                Subject = dr["Subject"].ToString(),
                                StartTime = dr["StartTime"] as DateTime? ?? DateTime.Now,
                                EndTime = dr["EndTime"] as DateTime? ?? DateTime.Now
                            }).ToList();
            }
            catch
            {
                //exceptions
            }
        }
    }
}
