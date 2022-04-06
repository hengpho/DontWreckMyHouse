using DontWreckMyHouse.Core.DTO;
using DontWreckMyHouse.Core.Exceptions;
using DontWreckMyHouse.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.DAL
{
    public class GuestFileRepository : IGuestRepository
    {
        private const string HEADER = "guest_id,first_name,last_name,email,phone,state";
        private readonly string filePath;

        public GuestFileRepository(string filePath)
        {
            this.filePath = filePath;
        }
        public List<Guest> FindAllGuest()
        {
            var guests = new List<Guest>();
            if (!File.Exists(filePath))
            {
                return guests;
            }
            string[] lines = null;
            try
            {
                lines = File.ReadAllLines(filePath);
            }
            catch (IOException ex)
            {
                throw new RepositoryException("could not read guests", ex);
            }

            for (int i = 1; i < lines.Length; i++) // skip the header
            {
                string[] fields = lines[i].Split(",", StringSplitOptions.TrimEntries);
                Guest guest = Deserialize(fields);
                if (guest != null)
                {
                    guests.Add(guest);
                }
            }
            return guests;
        }
        public Guest FindByEmail(string email)
        {
            var guests = FindAllGuest();
            return guests.FirstOrDefault(g => g.Email == email);
        }
        
        private Guest Deserialize(string[] fields)
        {
            if (fields.Length != 6)
            {
                return null;
            }
            Guest result = new Guest();
            result.Id = int.Parse(fields[0]);
            result.FirstName = fields[1];
            result.LastName = fields[2];
            result.Email = fields[3];
            result.Phone = fields[4];
            result.State = fields[5];
            return result;
        }
    }
}
