using DontWreckMyHouse.Core.DTO;
using DontWreckMyHouse.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.BLL.Tests.TestDouble
{
    public class ReservationRepositoryDouble : IReservationRepository
    {
        DateOnly startDate = new DateOnly(2022, 1, 12);
        DateOnly endDate = new DateOnly(2022, 1, 13);
        private readonly List<Reservation> reservations = new List<Reservation>();

        public ReservationRepositoryDouble()
        {
            Reservation reservation = new Reservation();
            reservation.Id = 124;
            reservation.StartDate = startDate;
            reservation.EndDate = endDate;
            reservation.Guest = GuestRepositoryDouble.GUEST;
            reservation.Host = HostRepositoryDouble.HOST;
            reservation.Total = 200;
            reservations.Add(reservation);
        }

        public Reservation AddReservation(Reservation reservation)
        {
            List<Reservation> all = FindByHost(reservation.Host.Id);
            int nextId = (all.Count == 0 ? 0 : all.Max(i => i.Id)) + 1;
            reservation.Id = nextId;
            reservations.Add(reservation);
            return reservation;
        }

        public bool Remove(Reservation reservation)
        {
            List<Reservation> all = FindByHost(reservation.Host.Id);
            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].Id == reservation.Id)
                {
                    all.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public List<Reservation> FindByHost(string hostId)
        {
            return reservations.Where(i => i.Host.Id == hostId).ToList();
        }

        public List<Reservation> GetAll()
        {
            return reservations;
        }

        public bool Update(Reservation reservation)
        {
            List<Reservation> all = FindByHost(reservation.Host.Id);
            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].Id == reservation.Id)
                {
                    all[i] = reservation;
                    return true;
                }
            }
            return false;
        }
    }   
}
