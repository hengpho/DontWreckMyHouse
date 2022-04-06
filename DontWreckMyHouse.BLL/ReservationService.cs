using DontWreckMyHouse.Core.DTO;
using DontWreckMyHouse.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.BLL
{
    public class ReservationService
    {
        private readonly IReservationRepository reservationRepository;
        private readonly IGuestRepository guestRepository;
        private readonly IHostRepository hostRepository;

        public ReservationService(IReservationRepository reservationRepository, IGuestRepository guestRepository, IHostRepository hostRepository)
        {
            this.reservationRepository = reservationRepository;
            this.guestRepository = guestRepository;
            this.hostRepository = hostRepository;
        }
        public List<Host> FindAllHost()
        {
            return hostRepository.FindAllHost();
        }
        public Host FindByHostEmail(string email){
            return hostRepository.FindByEmail(email);
        }
        public List<Guest> FindAllGuest()
        {
            return guestRepository.FindAllGuest();
        }
        public Guest FindByGuestEmail(string email)
        {
            return guestRepository.FindByEmail(email);
        }
        public List<Reservation> FindAllReservation(string hostId) //Find all Info within Hostfile
        {
            Dictionary<string, Host> hostMap = hostRepository.FindAllHost()
                    .ToDictionary(i => i.Id);
            Dictionary<int, Guest> guestMap = guestRepository.FindAllGuest()
                    .ToDictionary(i => i.Id);

            List<Reservation> result = reservationRepository.FindByHost(hostId);
            foreach (var reservation in result)
            {
                reservation.Host = hostMap[reservation.Host.Id];
                reservation.Guest = guestMap[reservation.Guest.Id];
            }
            return result;         
        }
        public Reservation AddReservation(Reservation reservation) 
        {
            //No Duplicate Reservation
            //
            return reservationRepository.AddReservation(reservation);
        }
        public bool Update(Reservation reservation)
        {
            return reservationRepository.Update(reservation);
        }
        public bool Delete(Reservation reservation)
        {
            return reservationRepository.Remove(reservation);
        }
    }
}

//getall.where x.startdate > DateTime.Now ?Done in view? maybe

//When adding reservation, only show future open dates. So dates that are not already on the list.