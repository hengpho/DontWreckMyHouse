using DontWreckMyHouse.Core.DTO;
using DontWreckMyHouse.Core.Exceptions;
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

        public Reservation AddReservation(Reservation reservation)
        {
            List<Reservation> all = FindByHost(reservation.Host.Id);
            int nextId = (all.Count == 0 ? 0 : all.Max(i => i.Id)) + 1;
            reservation.Id = nextId;
            all.Add(reservation);
            Write(all, reservation.Host.Id);
            return reservation;
        }

        public bool Update(Reservation reservation)
        {
            List<Reservation> all = FindByHost(reservation.Host.Id);
            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].Id == reservation.Id)
                {
                    all[i] = reservation;
                    Write(all, reservation.Host.Id);
                    return true;
                }
            }
            return false;
        }

        public bool Remove(Reservation reservation)
        {
            List<Reservation> all = FindByHost(reservation.Host.Id);
            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].Id == reservation.Id)
                {
                    all.RemoveAt(i);
                    Write(all, reservation.Host.Id);
                    return true;
                }
            }
            return false;
        }

        public List<Reservation> FindByHost(string hostId)
        {
            var hostName = new List<Reservation>();
            var path = GetFilePath(hostId);
            if (!File.Exists(path))
            {
                return hostName;
            }

            string[] lines = null;
            try
            {
                lines = File.ReadAllLines(path);
            }
            catch (IOException ex)
            {
                throw new RepositoryException("could not find host", ex);
            }


            for (int i = 1; i < lines.Length; i++) // skip the header
            {
                string[] fields = lines[i].Split(",", StringSplitOptions.TrimEntries);
                Reservation reservation = Deserialize(fields, hostId);
                if (reservation != null)
                {
                    hostName.Add(reservation);
                }
            }
            return hostName;
        }

        private string GetFilePath(string hostID)
        {
            return Path.Combine(directory, $"{hostID}.csv");
        }

        private string Serialize(Reservation reservation)
        {
            return string.Format("{0},{1},{2},{3},{4}",
                    reservation.Id,
                    reservation.StartDate,
                    reservation.EndDate,
                    reservation.Guest.Id, //write id of guest instead of just the guest
                    reservation.Total);
        }

        private Reservation Deserialize(string[] fields, string hostID)
        {
            if (fields.Length != 5)
            {
                return null;
            }

            Reservation result = new Reservation();
            result.Id = int.Parse(fields[0]);
            result.StartDate = DateOnly.Parse(fields[1]);
            result.EndDate = DateOnly.Parse(fields[2]);
            result.Total = decimal.Parse(fields[4]);

            Guest guest = new Guest();
            guest.Id = int.Parse(fields[3]);
            result.Guest = guest;

            Host host = new Host();
            host.Id = hostID;
            result.Host = host;

            return result;
        }

        private void Write(List<Reservation> reservations, string hostID)
        {
            try
            {
                using StreamWriter writer = new StreamWriter(GetFilePath(hostID));
                writer.WriteLine(HEADER);

                if (reservations == null)
                {
                    return;
                }

                foreach (var reservation in reservations)
                {
                    writer.WriteLine(Serialize(reservation));
                }
            }
            catch (IOException ex)
            {
                throw new RepositoryException("could not write forages", ex);
            }
        }
    }
}
