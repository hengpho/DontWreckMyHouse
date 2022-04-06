using DontWreckMyHouse.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.Core.Interfaces
{
    public interface IGuestRepository
    {
        public List<Guest> FindAllGuest();
        public Guest FindByEmail(string email);
    }
}
