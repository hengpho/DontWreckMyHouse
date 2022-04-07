using DontWreckMyHouse.Core.DTO;
using DontWreckMyHouse.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.BLL.Tests.TestDouble
{
    public class GuestRepositoryDouble : IGuestRepository
    {
        public readonly static Guest GUEST = MakeGuest();
        private readonly List<Guest> guests = new List<Guest>();

        public GuestRepositoryDouble()
        {
            guests.Add(GUEST);
        }
       
        private static Guest MakeGuest()
        {
            Guest guest = new Guest();
            guest.Id = 981;
            guest.FirstName = "Potato";
            guest.LastName = "Salad";
            guest.Email = "potatosalad@gmail.com";
            guest.Phone = "(321) 1234567";
            guest.State = "CA";
            return guest;
        }
        public List<Guest> FindAllGuest()
        {
            return guests;
        }
        public Guest FindByEmail(string email)
        {
            return guests.FirstOrDefault(h => h.Email == email);
        }
    }
}
