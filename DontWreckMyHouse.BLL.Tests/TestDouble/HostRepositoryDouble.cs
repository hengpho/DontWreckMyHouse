using DontWreckMyHouse.Core.DTO;
using DontWreckMyHouse.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.BLL.Tests.TestDouble
{
    public class HostRepositoryDouble : IHostRepository
    {
        public readonly static Host HOST = MakeHost();
        private readonly List<Host> hosts = new List<Host>();

        public HostRepositoryDouble()
        {
            hosts.Add(HOST);
        }

        private static Host MakeHost()
        {
            Host host = new Host();
            host.Id = "thisiste-t3st-t2st-fake-304dea8762db";
            host.LastName = "TestHost";
            host.Email = "testhost@gmail.com";
            host.Phone = "(123) 4567890";
            host.Address = "123 Test St";
            host.City = "Testville";
            host.State = "TestState";
            host.PostalCode = 1234;
            host.StandardRate = 100;
            host.WeekendRate = 150;
            return host;

        }
        public List<Host> FindAllHost()
        {
            return hosts;
        }

        public Host FindByEmail(string email)
        {
            return hosts.FirstOrDefault(h => h.Email == email);
        }
    }
}
