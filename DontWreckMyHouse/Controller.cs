using System;
using System.Collections.Generic;
using DontWreckMyHouse.BLL;

namespace DontWreckMyHouse.UI
{
    public class Controller
    {
        public readonly ReservationService reservationService;

        public Controller(ReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        public void Run()
        {

        }

        private void RunAppLoop()
        {
            MainMenuOption option;
            do
            {
                option = 
            }
        }

        
    } 
}
