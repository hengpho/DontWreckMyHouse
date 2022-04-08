using DontWreckMyHouse.BLL.Tests.TestDouble;
using DontWreckMyHouse.Core.DTO;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace DontWreckMyHouse.BLL.Tests
{
    public class ReservationServiceTest
    {
       ReservationService service = new ReservationService(
           new ReservationRepositoryDouble(),
           new GuestRepositoryDouble(),
           new HostRepositoryDouble());
        [Test]
        public void ShouldAdd()
        {
            Reservation reservation = new Reservation();
            reservation.Host = HostRepositoryDouble.HOST;
            reservation.Guest = GuestRepositoryDouble.GUEST;
            reservation.StartDate = new DateOnly(2022, 1, 12);
            reservation.EndDate = new DateOnly(2022, 1, 14);
            reservation.Total = 300m;

            Result<Reservation> result = service.AddReservation(reservation);
            List<Reservation> reservations = service.FindAllReservation(HostRepositoryDouble.HOST.Id);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(300, reservations[1].Total);
            Assert.AreEqual(2, reservations.Count);
            Assert.AreEqual(125, reservation.Id);
        }
        [Test]
        public void ShouldNotBeAbleToAddOverlappingDays()
        {
            Reservation reservation = new Reservation();
            reservation.Guest = GuestRepositoryDouble.GUEST;
            reservation.Host = HostRepositoryDouble.HOST;
            reservation.StartDate = new DateOnly(2022, 01, 12);
            reservation.EndDate = new DateOnly(2022, 01, 14);
            reservation.Total = 300;

            Result<Reservation> result = service.MakeReservation(reservation);

            Assert.IsFalse(result.Success);
        }
        [Test]
        public void ShouldBeAbleToUpdateAfterCheck()
        {
            Reservation reservation = new Reservation();
            reservation.Guest = GuestRepositoryDouble.GUEST;
            reservation.Host = HostRepositoryDouble.HOST;
            reservation.StartDate = new DateOnly(2022, 04, 11);
            reservation.EndDate = new DateOnly(2022, 04, 15);
            reservation.Id = 1;

            Result<Reservation> result = service.CheckB4Update(reservation);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(500, result.Value.Total);
        }
    }
}