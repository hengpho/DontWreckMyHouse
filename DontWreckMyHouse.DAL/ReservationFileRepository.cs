using DontWreckMyHouse.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace DontWreckMyHouse.DAL
{
    public class ReservationFileRepository : IReservationRepository
    {
        private const string HEADER = "id,start_date,end_date,guest_id,total";
        private readonly string directory;

        public ReservationFileRepository(string directory)
        {
            this.directory = directory;
        }
    }
}
