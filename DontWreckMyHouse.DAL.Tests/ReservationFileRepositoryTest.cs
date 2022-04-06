using DontWreckMyHouse.Core.DTO;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace DontWreckMyHouse.DAL.Tests
{
    public class Tests
    {

        const string SEED_FILE_PATH = @"C:\Users\aznlo\Projects\MasteryAssessment\DontWreckMyHouse.DAL.Tests\data\seed-file.csv";
        const string TEST_FILE_PATH = @"C:\Users\aznlo\Projects\MasteryAssessment\DontWreckMyHouse.DAL.Tests\data\reservations\2e72f86c-b8fe-4265-b4f1-304dea8762db.csv";
        const string TEST_DIR_PATH = @"C:\Users\aznlo\Projects\MasteryAssessment\DontWreckMyHouse.DAL.Tests\data\reservations\";
        const int RESERVATION_COUNT = 12;

        string hostID = "2e72f86c-b8fe-4265-b4f1-304dea8762db";
        DateOnly startDate = new DateOnly(2021, 1, 12);
        DateOnly endDate = new DateOnly(2021, 2, 12);

        ReservationFileRepository repository = new ReservationFileRepository(TEST_DIR_PATH);

        [SetUp]
        public void Setup()
        {
            File.Copy(SEED_FILE_PATH, TEST_FILE_PATH, true);
            File.Delete(@"C:\Users\aznlo\Projects\MasteryAssessment\DontWreckMyHouse.DAL.Tests\data\reservations\thisiste-t3st-t2st-fake-304dea8762db.csv");
        }

        [Test]
        public void ShouldFindByID()
        {
            List<Reservation> reservations = repository.FindByHost(hostID);
            Assert.AreEqual(RESERVATION_COUNT, reservations.Count);
        }
        [Test]
        public void ShouldAdd()
        {

            Reservation reservation = new();
            reservation.StartDate = startDate;
            reservation.EndDate = endDate;
            reservation.Total = 100m; //Change later

            Guest guest = new();
            guest.Id = 543;

            Host host = new();
            host.Id = hostID;

            reservation.Host = host;
            reservation.Guest = guest;

            repository.AddReservation(reservation);
            List<Reservation> reservations = repository.FindByHost(hostID);
            Assert.AreEqual(RESERVATION_COUNT + 1, reservations.Count);
        }
        [Test]
        public void ShouldWriteNewFile()
        {
            Reservation reservation = new();
            reservation.StartDate = startDate;
            reservation.EndDate = endDate;
            reservation.Total = 100m; //Change later

            Guest guest = new();
            guest.Id = 543;

            Host host = new();
            host.Id = "thisiste-t3st-t2st-fake-304dea8762db";

            reservation.Host = host;
            reservation.Guest = guest;

            repository.AddReservation(reservation);
            List<Reservation> reservations = repository.FindByHost(host.Id);
            Assert.AreEqual(1, reservations.Count);
        }
        [Test]
        public void ShouldUpdate()
        {
            DateOnly startDate2 = new DateOnly(2021, 1, 13);
            DateOnly endDate2 = new DateOnly(2021, 2, 13);
            Reservation reservation = new();
            reservation.Id = 1;
            reservation.StartDate = startDate2;
            reservation.EndDate = endDate2;
            reservation.Total = 100m;
            

            Guest guest = new();
            guest.Id = 663;

            Host host = new();
            host.Id = hostID;

            reservation.Host = host;
            reservation.Guest = guest;

            bool status = repository.Update(reservation);
            List<Reservation> reservations = repository.FindByHost(hostID);

            Assert.IsTrue(status);
            Assert.AreEqual(reservations[0].Total, 100);
        }
        [Test]
        public void ShouldNotUpdate()
        {
            DateOnly startDate2 = new DateOnly(2021, 1, 13);
            DateOnly endDate2 = new DateOnly(2021, 2, 13);
            Reservation reservation = new();
            reservation.Id = 12312;
            reservation.StartDate = startDate2;
            reservation.EndDate = endDate2;
            reservation.Total = 100m;


            Guest guest = new();
            guest.Id = 663;

            Host host = new();
            host.Id = hostID;

            reservation.Host = host;
            reservation.Guest = guest;

            bool status = repository.Update(reservation);
            List<Reservation> reservations = repository.FindByHost(hostID);

            Assert.IsFalse(status);
        }

        [Test]
        public void ShouldRemove()
        {
            Reservation reservation = new();
            reservation.Id = 2;
            reservation.StartDate = new DateOnly(2021,9,10);
            reservation.EndDate = new DateOnly(2021, 9, 16);
            reservation.Total = 1300m;


            Guest guest = new();
            guest.Id = 136;

            Host host = new();
            host.Id = hostID;

            reservation.Host = host;
            reservation.Guest = guest;

            bool status = repository.Remove(reservation);
            List<Reservation> reservations = repository.FindByHost(hostID);

            Assert.IsTrue(status);
            Assert.AreEqual(RESERVATION_COUNT - 1, reservations.Count);
        }
        [Test]
        public void ShouldNotRemove()
        {
            Reservation reservation = new();
            reservation.Id = 929493;
            reservation.StartDate = new DateOnly(2021, 9, 10);
            reservation.EndDate = new DateOnly(2021, 9, 16);
            reservation.Total = 1300m;


            Guest guest = new();
            guest.Id = 136;

            Host host = new();
            host.Id = hostID;

            reservation.Host = host;
            reservation.Guest = guest;

            bool status = repository.Remove(reservation);
            List<Reservation> reservations = repository.FindByHost(hostID);

            Assert.IsFalse(status);
        }
    }
}