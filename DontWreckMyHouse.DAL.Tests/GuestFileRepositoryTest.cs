using DontWreckMyHouse.Core.DTO;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace DontWreckMyHouse.DAL.Tests
{
    public class GuestFileRepositoryTest
    {
        const string TEST_DIR_PATH = @"C:\Users\aznlo\Projects\MasteryAssessment\DontWreckMyHouse.DAL.Tests\data\guests.csv";
        GuestFileRepository repo = new GuestFileRepository(TEST_DIR_PATH);

        [Test]
        public void ShouldFindGuests()
        {
            List<Guest> guests = repo.FindAllGuest();
            Assert.AreEqual(1000, guests.Count);
        }
        [Test]
        public void ShouldFindGuestByEmail()
        {
            List<Guest> guests = repo.FindAllGuest();
            Guest guest = repo.FindByEmail("tcarncross2@japanpost.jp");
            Assert.AreEqual(guests[2].Email, guest.Email);
        }
    }
}
