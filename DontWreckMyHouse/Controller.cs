using System;
using System.Collections.Generic;
using DontWreckMyHouse.BLL;

namespace DontWreckMyHouse.UI
{
    public class Controller
    {
        public readonly ReservationService reservationService;
        private readonly View view;

        public Controller(ReservationService reservationService, View view)
        {
            this.reservationService = reservationService;
            this.view = view;
        }

        public void Run()
        {
            view.DisplayHeader("Welcome to Don't Wreck My House");
        }

        private void RunAppLoop()
        {
            MainMenuOption option;
            do
            {
                option = view.SelectMainMenuOption();
                switch (option)
                {
                    case MainMenuOption.ViewReservation:
                        throw new NotImplementedException();
                        break;
                }
            } while(option != MainMenuOption.Exit);
        }

        
    } 
}
