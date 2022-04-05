using Ninject;
using DontWreckMyHouse.BLL;
using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.DAL;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.UI
{
    internal class NinjectContainer
    {
        public static StandardKernel kernel { get; private set; }

        public static void Configure()
        {
            kernel = new StandardKernel();

            kernel.Bind<ConsoleIO>().To<ConsoleIO>();
            kernel.Bind<View>().To<View>();

            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string reservationFileDirectory = Path.Combine(projectDirectory, "data", "reservations");
            string guestsFilePath = Path.Combine(projectDirectory, "data", "guests.csv");
            string hostsFilePath = Path.Combine(projectDirectory, "data", "hosts.csv");

            kernel.Bind<IReservationRepository>().To<ReservationFileRepository>().WithConstructorArgument(reservationFileDirectory);
            kernel.Bind<IGuestRepository>().To<GuestFileRepository>().WithConstructorArgument(guestsFilePath);
            kernel.Bind<IHostRepository>().To<HostFileRepository>().WithConstructorArgument(hostsFilePath);

            kernel.Bind<ReservationService>().To<ReservationService>();
        }
    }
}
