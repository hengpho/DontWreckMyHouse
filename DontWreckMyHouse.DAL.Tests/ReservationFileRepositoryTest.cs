using NUnit.Framework;
using System;

namespace DontWreckMyHouse.DAL.Tests
{
    public class Tests
    {

        const string SEED_FILE_PATH = @"C:\Users\aznlo\Projects\MasteryAssessment\DontWreckMyHouse.DAL.Tests\data\seed-file.csv";
        const string TEST_FILE_PATH = @"C:\Users\aznlo\Projects\MasteryAssessment\DontWreckMyHouse.DAL.Tests\data\reservations\2e72f86c-b8fe-4265-b4f1-304dea8762db.csv";
        const string TEST_DIR_PATH = @"C:\Users\aznlo\Projects\MasteryAssessment\DontWreckMyHouse.DAL.Tests\data\";
        const int RESERVATION_COUNT = 12;

        DateTime startDate = new DateTime(2021, 1, 12);
        DateTime endDate = new DateTime(2021, 2, 12);

        ReservationFileRepository repository = new ReservationFileRepository(TEST_DIR_PATH);

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}