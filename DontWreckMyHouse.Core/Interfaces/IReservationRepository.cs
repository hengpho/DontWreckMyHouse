using System;
using System.Collections.Generic;
using DontWreckMyHouse.Core.DTO;

namespace DontWreckMyHouse.Core.Interfaces
{
    public interface IReservationRepository
    {
        public Reservation AddReservation(Reservation reservation);
        public bool Update(Reservation reservation);
        public bool Remove(Reservation reservation);
        public List<Reservation> FindByHost(string hostId);       
    }
}
