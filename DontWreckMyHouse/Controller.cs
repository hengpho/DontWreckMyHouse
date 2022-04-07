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
                        CancleReservation();
                        break;
                }
            } while(option != MainMenuOption.Exit);
        }

        private void ViewReservation()
        { 
            var host = GetHost();
            List<Reservation> reservations = reservationService.FindAllReservation(host.Id);
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
            Result<Reservation> result = reservationService.MakeReservation(reservation);
            if (!result.Success)
            {
                view.DisplayStatus(false, result.Messages);
            }
            else
            {
                if (view.DisplayTotalPrompt(result))
                {
                    Result<Reservation> resultToAdd = reservationService.AddReservation(reservation);
                    string successMessage = $"Reservation {resultToAdd.Value.Id} created.";
                    view.DisplayStatus(true, successMessage);
                }
                else
                {
                    return;
                }
                
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
        private void EditReservation()
        {
            view.DisplayHeader(MainMenuOption.EditReservation.ToLabel());
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

        }
        private void CancleReservation()
        {
            view.DisplayHeader(MainMenuOption.CancelReservation.ToLabel());
            Host host = GetHost();
            List<Reservation> reservations = reservationService.FindAllReservation(host.Id);
            if (!view.DisplayReservations4Host(reservations))
            {
                return;
            }
            Guest guest = GetGuest();
            if (guest == null)
            {
                return;
            }
            view.DisplayHeader($"Displaying all reservations for {host.LastName} - {host.Email}");
            if (view.DisplayFutureReservations(reservations))
            {
                Reservation reservation = view.DeleteReservation(host, guest);
                bool result = reservationService.Delete(reservation);
                if (!result)
                {
                    string failMessage = $"Reservation could not be found";
                    view.DisplayStatus(false, failMessage);
                }
                else
                {
                    string successMessage = $"Reservation {reservation.Id} cancelled.";
                    view.DisplayStatus(true, successMessage);
                }
            }
            else
            {
                return;
            }
        }

    } 
}
