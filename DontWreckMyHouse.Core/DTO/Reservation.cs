using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.Core.DTO
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guest Guest { get; set; }
        public Host Host { get; set; }
        public decimal Total { get; set; }
        
        public decimal GetTotal()
        {
            /*decimal weekendPrice = 0M;
            decimal weekdayPrice = 0M;
            for (var day = StartDate.Date; day <= EndDate.Date; day = day.AddDays(1))
            {
                if (day.DayOfWeek == DayOfWeek.Sunday || day.DayOfWeek == DayOfWeek.Saturday)
                {
                    weekendPrice = weekendPrice + Host.WeekendRate;
                }
                else
                {
                    weekdayPrice = weekdayPrice + Host.StandardRate;
                }
            }
            decimal cost = weekdayPrice + weekendPrice;
            return cost;*/
            throw new NotImplementedException();
        }
    }
}
