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
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public Guest Guest { get; set; }
        public Host Host { get; set; }
        public decimal Total { get; set; }
        
        public decimal GetTotal()
        {
            decimal weekendPrice = 0M;
            decimal weekdayPrice = 0M;
            for (var day = StartDate; day <= EndDate; day = day.AddDays(1))
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
            return cost;
        }
    }
}

// Reservation reservation = (reservations id start date etc)
// reservation.GetTotal()