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
    }
}
