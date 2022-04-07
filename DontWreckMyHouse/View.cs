using DontWreckMyHouse.BLL;
using DontWreckMyHouse.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.UI
{
    public class View
    {
        private readonly ConsoleIO io;

        public View(ConsoleIO io)
        {
            this.io = io;
        }
        public MainMenuOption SelectMainMenuOption()
        {
            DisplayHeader("Main Menu");
            int min = int.MaxValue;
            int max = int.MinValue;
            MainMenuOption[] options = Enum.GetValues<MainMenuOption>();
            for (int i = 0; i < options.Length; i++)
            {
                MainMenuOption option = options[i];
                io.PrintLine($"{i}. {option.ToLabel()}");
                min = Math.Min(min, i);
                max = Math.Max(max, i);
            }

            string message = $"Select [{min}-{max}]: ";
            return options[io.ReadInt(message, min, max)];
        }

        public void DisplayHeader(string message)
        {
            io.PrintLine("");
            io.PrintLine(message);
            io.PrintLine(new string('=', message.Length));
        }
        public string GetLastNamePrefix(string message)
        {
            return io.ReadRequiredString($"Enter {message} last name starting with: ");
        }

        public void DisplayException(Exception ex)
        {
            DisplayHeader("A critical error occurred:");
            io.PrintLine(ex.Message);
        }
        public void DisplayStatus(bool success, string message)
        {
            DisplayStatus(success, new List<string>() { message });
        }
        public void DisplayStatus(bool success, List<string> messages)
        {
            DisplayHeader(success ? "Success" : "Error");
            foreach (string message in messages)
            {
                io.PrintLine(message);
            }
        }

        public string GetHostEmail()
        {
            return io.ReadRequiredString("Enter host email address: ");
        }
        public void EnterToContinue()
        {
            io.ReadString("Press [Enter] to continue.");
        }

        public void DisplayReservations4Host(List<Reservation> reservations)
        {
            if (reservations == null || reservations.Count == 0)
            {
                io.PrintLine("Host has no reservations.");
                return;
            }

            var orderedReservations = reservations.OrderBy(r => r.StartDate);
            foreach (Reservation reservation in orderedReservations)
            {
                io.PrintLine(
                    string.Format("ID: {0}, {1} - {2}, Guest: {3}, {4}, Email: {5}",
                        reservation.Id,
                        reservation.StartDate,
                        reservation.EndDate,
                        reservation.Guest.FirstName,
                        reservation.Guest.LastName,
                        reservation.Guest.Email));
            }
        }
        public void DisplayReservations(List<Reservation> reservations)
        {
            if (reservations == null || reservations.Count == 0)
            {
                io.PrintLine("Host has no reservations.");
                return;
            }
            var orderedReservations = reservations.OrderBy(r => r.StartDate);
            foreach (Reservation reservation in orderedReservations)
            {
                io.PrintLine(
                    string.Format("ID: {0}, {1} - {2}",
                        reservation.Id,
                        reservation.StartDate,
                        reservation.EndDate));
            }
        }

        public bool DisplayTotalPrompt(Result<Reservation> result)
        {
            DisplayHeader("Summary");
                io.PrintLine(
                    string.Format("Start: {0}\nEnd {1}\nTotal: {2:C2}",
                        result.Value.StartDate,
                        result.Value.EndDate,
                        result.Value.Total));

            if(io.ReadBool("Is this Okay? [y/n]: "))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public Host ChooseHosts(List<Host> allHost)
        {
            if (allHost == null || allHost.Count == 0)
            {
                io.PrintLine("No Host found");
                return null;
            }

            int index = 1;
            foreach (Host host in allHost.Take(25))
            {
                io.PrintLine($"{index++}: Host:{host.LastName} [{host.City},{host.State}] {host.Email}, Standard Rate:{host.StandardRate:c} - Weekend Rate:{host.WeekendRate:c}");
            }
            index--;

            if (allHost.Count > 25)
            {
                io.PrintLine("More than 25 Host found. Showing first 25. Please refine your search.");
            }
            io.PrintLine("0: Exit");
            string message = $"Select a Host by their index [0-{index}]: ";

            index = io.ReadInt(message, 0, index);
            if (index <= 0)
            {
                return null;
            }
            return allHost[index - 1];
        }

        public Reservation MakeReservation(Host host, Guest guest)
        {
            Reservation reservation = new Reservation();
            reservation.Guest = guest;
            reservation.Host = host;
            reservation.StartDate = io.ReadDate("Enter Start Date (mm/dd/yyyy): ");
            reservation.EndDate = io.ReadDate("Enter End Date (mm/dd/yyyy): ");

            return reservation;
        }

        public Guest ChooseGuests(List<Guest> allGuest)
        {
            if (allGuest == null || allGuest.Count == 0)
            {
                io.PrintLine("No Guest found");
                return null;
            }
            
            int index = 1;
            foreach (Guest guest in allGuest.Take(25))
            {
                io.PrintLine($"{index++}: {guest.FirstName} {guest.LastName} {guest.Email} {guest.State}");
            }
            index--;

            if (allGuest.Count > 25)
            {
                io.PrintLine("More than 25 Guest found. Showing first 25. Please refine your search.");
            }
            io.PrintLine("0: Exit");
            string message = $"Select a Guest by their index [0-{index}]: ";

            index = io.ReadInt(message, 0, index);
            if (index <= 0)
            {
                return null;
            }
            return allGuest[index - 1];
        }
    }
}
