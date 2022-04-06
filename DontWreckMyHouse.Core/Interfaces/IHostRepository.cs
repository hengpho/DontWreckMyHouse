﻿using DontWreckMyHouse.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.Core.Interfaces
{
    public interface IHostRepository
    {
        public List<Host> FindAllHost();
        public Host FindByEmail(string email);
    }
}
