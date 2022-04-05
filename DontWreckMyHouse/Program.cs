using Ninject;
using System;
using System.IO;

namespace DontWreckMyHouse.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            NinjectContainer.Configure();
            Controller controller = NinjectContainer.kernel.Get<Controller>();
            controller.Run();
        }

    }
}