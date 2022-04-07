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
        public List<Host> FindByHostLastName(string prefix)
        {
            return hostRepository.FindAllHost()
                .Where(h => h.LastName.StartsWith(prefix))
                .ToList();
        }
        public List<Guest> FindAllGuest()
        {
            return guestRepository.FindAllGuest();
        }
        public Guest FindByGuestEmail(string email)
        {
            return guestRepository.FindByEmail(email);
        }
        public List<Guest> FindByGuestLastName(string prefix)
        {
            return guestRepository.FindAllGuest()
                .Where(g => g.LastName.StartsWith(prefix))
                .ToList();
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
                    //Back to ui                        to DAL
        public Result<Reservation> AddReservation(Reservation reservation)
        {
            Result<Reservation> result = new();
            result.Value = reservationRepository.AddReservation(reservation);
            return result;
        }
        public Result<Reservation> MakeReservation(Reservation reservation)
        {
            Result<Reservation> result = Validate(reservation);
            if (!result.Success)
            {
                return result;
            }
            reservation.Total = reservation.GetTotal();
            result.Value = reservation;
            return result;
        }
        public bool Update(Reservation reservation)
        {
            return reservationRepository.Update(reservation);
        }
        public bool Delete(Reservation reservation)
        {
            return reservationRepository.Remove(reservation);
        }
        private Result<Reservation> Validate(Reservation reservation)
        {
            Result<Reservation> result = ValidateNulls(reservation);
            if (!result.Success)
            {
                return result;
            }

            ValidateFields(reservation, result);
            if (!result.Success)
            {
                return result;
            }

            ValidateChildrenExist(reservation, result);
            if (!result.Success)
            {
                return result;
            }

            List<Reservation> reservations = reservationRepository.FindByHost(reservation.Host.Id);
            var currentReservation = reservations.FirstOrDefault(
                x => (x.StartDate <= reservation.StartDate && x.EndDate >= reservation.StartDate)
                || (x.StartDate <= reservation.EndDate && x.EndDate >= reservation.EndDate));

            if (currentReservation != null)
            {
                result.AddMessage($"Reservation dates overlap with reservation {currentReservation.Id}, {currentReservation.StartDate} - {currentReservation.EndDate}");
                return result;
            }

            return result;
        }

        private Result<Reservation> ValidateNulls(Reservation reservation)
        {
            Result<Reservation> result = new Result<Reservation>();
            if (reservation == null)
            {
                result.AddMessage("Reservation is null.");
                return result;
            }
            if (reservation.Host == null)
            {
                result.AddMessage("Host is null.");
                return result;
            }
            if (reservation.Guest == null)
            {
                result.AddMessage("Guest is null.");
                return result;
            }
            return result;
        }
        private void ValidateFields(Reservation reservation, Result<Reservation> result)
        {
            if ( reservation.StartDate < DateOnly.FromDateTime(DateTime.Now))
            {
                result.AddMessage("Date cannot be in the past.");
            }
            if (reservation.EndDate < reservation.StartDate)
            {
                result.AddMessage("End date cannot be before start date.");
            }
        }

        private void ValidateChildrenExist(Reservation reservation, Result<Reservation> result)
        {
            if (reservation.Host.Id == null
                    || hostRepository.FindByEmail(reservation.Host.Email) == null)
            {
                result.AddMessage("Host does not exist.");
            }

            if (reservation.Host.Id == null
                    || guestRepository.FindByEmail(reservation.Guest.Email) == null)
            {
                result.AddMessage("Guest does not exist.");
            }
        }
    }
}