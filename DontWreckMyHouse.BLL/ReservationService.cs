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
    }
}