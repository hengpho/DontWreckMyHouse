using System;
using System.Collections.Generic;
using DontWreckMyHouse.BLL;
using DontWreckMyHouse.Core.DTO;
using DontWreckMyHouse.Core.Exceptions;

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
            try
            {
                RunAppLoop();
            }
            catch (RepositoryException ex)
            {
                view.DisplayException(ex);
            }
            view.DisplayHeader("Goodbye.");
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
                        ViewReservation();
                        break;
                    case MainMenuOption.MakeReservation:
                        Console.WriteLine("MakeReservation");
                        break;
                    case MainMenuOption.EditReservation:
                        Console.WriteLine("EditReservation");
                        break;
                    case MainMenuOption.CancelReservation:
                        Console.WriteLine("CancelReservation");
                        break;
                }
            } while(option != MainMenuOption.Exit);
        }

        private void ViewReservation()
        { //swhettletoncj@google.pl -- Testing Email
            string email = view.GetHostEmail();
            Host hostEmail = reservationService.FindByHostEmail(email);
            List<Reservation> reservations = reservationService.FindAllReservation(hostEmail.Id);
            view.DisplayReservations(reservations);
            view.EnterToContinue();
        }
        private void MakeReservation()
        {

        }
        private void EditReservation()
        {

        }
        private void CancleReservation()
        {

        }

    } 
}
