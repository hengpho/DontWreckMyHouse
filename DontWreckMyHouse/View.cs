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

        public void DisplayException(Exception ex)
        {
            DisplayHeader("A critical error occurred:");
            io.PrintLine(ex.Message);
        }

        public string GetHostEmail()
        {
            return io.ReadRequiredString("Enter host email address: ");
        }
        public void EnterToContinue()
        {
            io.ReadString("Press [Enter] to continue.");
        }

        public void DisplayReservations(List<Reservation> reservations)
        {
            if (reservations == null || reservations.Count == 0)
            {
                io.PrintLine("No Reservation found.");
                return;
            }

            foreach (Reservation reservation in reservations)
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
    }
}
