using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.Core.DTO
{
    public class Host
    {
        public string Id { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set;  }
        public string City { get; set; }
        public string State { get; set; }
        public int PostalCode { get; set; }
        public decimal StandardRate { get; set; }
        public decimal WeekendRate { get; set; }
    }
}
