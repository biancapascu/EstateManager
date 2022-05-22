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
    class FrequencyArrayItem
    {
        public string name { get; set; }
        public int count { get; set; }
        public FrequencyArrayItem(string name, int count)
        {
            this.name = name;
            this.count = count;
        }
    }

    public partial class HomeView : UserControl
    {
        public List<Reservations> Reservations { get; set; }
        private string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        public static string username { get; set; }
        public HomeView()
        {
            InitializeComponent();
            DataContext = this;
            InitializeChart();
            InitializeStatisticNumbers();
        }

        private void InitializeChart()
        {
            var dataTable = ConnectDB.GetDataTable("SELECT * FROM Reservations");
            DataRowCollection rows = dataTable.Rows;
            int numberOfLocations = ConnectDB.GetDataTable("SELECT * FROM Accomodation").Rows.Count;
            double[] chartValues = new double[12];
            int[] numbeOfReservationsPerMonth = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            foreach (DataRow row in rows)
            {
                if (Convert.ToDateTime(row.ItemArray[1]).Year == DateTime.Now.Year || Convert.ToDateTime(row.ItemArray[2]).Year == DateTime.Now.Year)
                {
                    DateTime startTime = Convert.ToDateTime(row.ItemArray[1]);
                    DateTime endTime = Convert.ToDateTime(row.ItemArray[2]);
                    int numberOfDays = Convert.ToInt32((endTime - startTime).TotalDays);
                    if (startTime.Month == endTime.Month)
                    {
                        numbeOfReservationsPerMonth[startTime.Month] += numberOfDays;
                    }
                    else
                    {
                        int startingMonth = startTime.Month;
                        int endingMonth = endTime.Month;
                        for (int currentMonth = startingMonth; currentMonth <= endingMonth; currentMonth++)
                        {
                            int daysInCurrentMonth = DateTime.DaysInMonth(DateTime.Now.Year, currentMonth);
                            if (currentMonth == startingMonth)
                            {
                                numbeOfReservationsPerMonth[currentMonth] += daysInCurrentMonth - startTime.Day;
                            }
                            else if (currentMonth == endingMonth)
                            {
                                numbeOfReservationsPerMonth[currentMonth] += endTime.Day;
                            }
                            else
                            {
                                numbeOfReservationsPerMonth[currentMonth] += daysInCurrentMonth;
                            }
                        }
                    }
                }
            }

            for (int index = 0; index < numbeOfReservationsPerMonth.Length; index++)
            {
                int daysInCurrentMonth = DateTime.DaysInMonth(DateTime.Now.Year, index + 1);
                int maxDays = daysInCurrentMonth * numberOfLocations;
                double percentage = Convert.ToDouble(numbeOfReservationsPerMonth[index]) / Convert.ToDouble(maxDays) * 100;
                percentage = Math.Truncate(percentage * 1000) / 1000;
                chartValues[index] = percentage;
            }

            var convertedValues = new ChartValues<double>(chartValues);
            reservationsChartData.Values = convertedValues;
        }

        private void InitializeStatisticNumbers()
        {
            DataTable dataTable = ConnectDB.GetDataTable("SELECT * FROM Records WHERE YEAR(StartTime) = YEAR(getdate()) OR YEAR(EndTime) = YEAR(getdate())");
            int totalBookingsThisYear = dataTable.Rows.Count;
            int numberOfLocations = ConnectDB.GetDataTable("SELECT * FROM Accomodation").Rows.Count;
            DataRowCollection accomodationsRowsName = ConnectDB.GetDataTable("SELECT Name FROM Accomodation").Rows;
            List<string> locations = new List<string>();
            DataRowCollection extrasRows = ConnectDB.GetDataTable("SELECT Name, Quantity from Extras").Rows;
            List<FrequencyArrayItem> locationsFrequencyArray = new List<FrequencyArrayItem>();
            List<FrequencyArrayItem> extrasFrequencyArray = new List<FrequencyArrayItem>();


            foreach (DataRow row in accomodationsRowsName)
            {
                locations.Add(Convert.ToString(row.ItemArray[0]));
            }

            foreach (string location in locations)
            {
                FrequencyArrayItem item = new FrequencyArrayItem(location, 0);
                locationsFrequencyArray.Add(item);
            }

            dataTable = ConnectDB.GetDataTable("SELECT * FROM Reservations");
            DataRowCollection rows = dataTable.Rows;

            foreach (DataRow row in rows)
            {
                string location = Convert.ToString(row.ItemArray[3]);
                int indexToIncrease = locationsFrequencyArray.FindIndex(x => x.name == location);
                locationsFrequencyArray.ToArray()[indexToIncrease].count++;
            }

            int max = 0;
            string mostBookedLocation = "";
            foreach (FrequencyArrayItem item in locationsFrequencyArray)
            {
                if (item.count > max)
                {
                    max = item.count;
                    mostBookedLocation = item.name;
                }
            }

            foreach (DataRow row in extrasRows)
            {
                string name = Convert.ToString(row.ItemArray[0]);
                int quantity = Convert.ToInt32(row.ItemArray[1]);
                if (extrasFrequencyArray.Exists(x => x.name == name))
                {
                    int indexToFind = extrasFrequencyArray.FindIndex(x => x.name == name);
                    extrasFrequencyArray.ToArray()[indexToFind].count += quantity;
                }
                else
                {
                    FrequencyArrayItem item = new FrequencyArrayItem(name, quantity);
                    extrasFrequencyArray.Add(item);
                }

            }

            int maxExtra = 0;
            string mostRequestedExtra = "";
            foreach (FrequencyArrayItem item in extrasFrequencyArray)
            {
                if (item.count > maxExtra)
                {
                    maxExtra = item.count;
                    mostRequestedExtra = item.name;
                }
            }

            ChartValues<double> chartValues = (ChartValues<double>)reservationsChartData.Values;
            int mostBookedMonth = -1;
            double maxMonthValue = 0;
            foreach (double monthValue in chartValues)
            {
                int index = chartValues.IndexOf(monthValue);
                if (monthValue > maxMonthValue)
                {
                    mostBookedMonth = index;
                }
            }

            mostWanted.Text = mostBookedLocation;
            totalBookings.Text = totalBookingsThisYear.ToString();
            topService.Text = mostRequestedExtra;
            peakSeason.Text = months[mostBookedMonth - 1];

        }
    }
}
