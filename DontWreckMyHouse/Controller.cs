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
                        MakeReservation();
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
            view.DisplayHeader(MainMenuOption.ViewReservation.ToLabel());
            string email = view.GetHostEmail();
            Host hostEmail = reservationService.FindByHostEmail(email);
            List<Reservation> reservations = reservationService.FindAllReservation(hostEmail.Id);
            view.DisplayReservations4Host(reservations);
            view.EnterToContinue();
        }
        private void MakeReservation()
        { //3edda6bc-ab95-49a8-8962-d50b53f84b15,Yearnes,eyearnes0@sfgate.com,(806) 1783815,3 Nova Trail,Amarillo,TX,79182,340,425
            view.DisplayHeader(MainMenuOption.MakeReservation.ToLabel());
            Host host = GetHost();
            if (host == null)
            {
                return;
            }
            Guest guest = GetGuest();
            if (guest == null)
            {
                return;
            }
            List<Reservation> reservations = reservationService.FindAllReservation(host.Id);
            view.DisplayHeader($"Displaying all reservations for {host.LastName} - {host.Email}");
            view.DisplayReservations(reservations);
            Reservation reservation = view.MakeReservation(host, guest);
            Result<Reservation> result = reservationService.AddReservation(reservation);
            if (!result.Success)
            {
                view.DisplayStatus(false, result.Messages);
            }
            else
            {
                string successMessage = $"Reservation {result.Value.Id} created.";
                view.DisplayStatus(true, successMessage);
            }
        }
        private Host GetHost()
        {
            string LastNamePrefix = view.GetLastNamePrefix("Host");
            List<Host> allHost = reservationService.FindByHostLastName(LastNamePrefix);
            return view.ChooseHosts(allHost);

        }
        private Guest GetGuest()
        {
            string LastNamePrefix = view.GetLastNamePrefix("Guest");
            List<Guest> allGuest = reservationService.FindByGuestLastName(LastNamePrefix);
            return view.ChooseGuests(allGuest);
        }
        private void AddReservation()
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
