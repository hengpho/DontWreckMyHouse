using DontWreckMyHouse.BLL.Tests.TestDouble;
using NUnit.Framework;

namespace DontWreckMyHouse.BLL.Tests
{
    public class ReservationServiceTest
    {
       ReservationService service = new ReservationService(
           new ReservationRepositoryDouble(),
           new GuestRepositoryDouble(),
           new HostRepositoryDouble());
        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}