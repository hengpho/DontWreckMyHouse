using DontWreckMyHouse.Core.DTO;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace DontWreckMyHouse.DAL.Tests
{
    public class HostFileRepositoryTest
    {
        const string TEST_DIR_PATH = @"C:\Users\aznlo\Projects\MasteryAssessment\DontWreckMyHouse.DAL.Tests\data\hosts.csv";
        HostFileRepository repo = new HostFileRepository(TEST_DIR_PATH);
        
        [Test]
        public void ShouldFindHost()
        {
            List<Host> hosts = repo.FindAllHost();
            Assert.AreEqual(1000, hosts.Count);
        }
        [Test]
        public void ShouldFindHostByEmail()
        {
            List<Host> hosts = repo.FindAllHost();
            Host host = repo.FindByEmail("mfader2@amazon.co.jp");
            Assert.AreEqual(hosts[2].Email, host.Email);
        }
    }
}
