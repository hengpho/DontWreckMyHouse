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
    public class HostFileRepository : IHostRepository
    {
        private const string HEADER = "id,last_name,email,phone,address,city,state,postal_code,standard_rate,weekend_rate";
        private readonly string filePath;

        public HostFileRepository(string filePath)
        {
            this.filePath = filePath;
        }
        public List<Host> FindAllHost()
        {
            var hosts = new List<Host>();
            if (!File.Exists(filePath))
            {
                return hosts;
            }
            string[] lines = null;
            try
            {
                lines = File.ReadAllLines(filePath);
            }
            catch (IOException ex)
            {
                throw new RepositoryException("could not read hosts", ex);
            }

            for (int i = 1; i < lines.Length; i++) // skip the header
            {
                string[] fields = lines[i].Split(",", StringSplitOptions.TrimEntries);
                Host host = Deserialize(fields);
                if (host != null)
                {
                    hosts.Add(host);
                }
            }
            return hosts;
        }
        public Host FindByEmail(string email)
        {
            var hosts = FindAllHost();
            return hosts.FirstOrDefault(h => h.Email == email);
        }
        private Host Deserialize(string[] fields)
        {
            if (fields.Length != 10)
            {
                return null;
            }
            Host result = new Host();
            result.Id = fields[0];
            result.LastName = fields[1];
            result.Email = fields[2];
            result.Phone = fields[3];
            result.Address = fields[4];
            result.City = fields[5];
            result.State = fields[6];
            result.PostalCode = int.Parse(fields[7]);
            result.StandardRate = decimal.Parse(fields[8]);
            result.WeekendRate = decimal.Parse(fields[9]);
            return result;
        }
    }   
}
