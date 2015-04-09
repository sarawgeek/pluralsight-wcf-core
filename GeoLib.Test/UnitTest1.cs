using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GeoLib.Data;
using GeoLib.Contracts;
using GeoLib.Services;
using System.Collections.Generic;
using System.Linq;


namespace GeoLib.Test
{
    [TestClass]
    public class ManagerTests
    {
        [TestMethod]
        public void test_zip_code_retrieval()
        {
            Mock<IZipCodeRepository> mockZipCodeRepository = new Mock<IZipCodeRepository>();
            ZipCode zipCode = new ZipCode()
            {
                City = "Lincoln Park",
                State = new State() { Abbreviation = "NJ" },
                Zip = "07035"
            };
            mockZipCodeRepository.Setup(obj => obj.GetByZip("07035")).Returns(zipCode);
            IGeoService geoService = new GeoManager(mockZipCodeRepository.Object);
            ZipCodeData data = geoService.GetZipInfo("07035");
            Assert.IsTrue(data.City.ToUpper() == "LINCOLN PARK");
        }
        [TestMethod]
        public void test_states_retrieval()
        {
            Mock<IStateRepository> mockStateRepository = new Mock<IStateRepository>();

            State state = new State()
            {
                Abbreviation = "NJ",
                IsPrimaryState=true,
                Name="New Jersey"
            };
            List<State> stateList = new List<State>(){state};
            mockStateRepository.Setup(obj => obj.Get(true)).Returns(stateList);
            IGeoService geoService = new GeoManager(mockStateRepository.Object);
            List<string> data = geoService.GetStates(true).ToList();            
            Assert.IsTrue(data[0] == "NJ");
        }
        [TestMethod]
        public void test_retrieve_zip_by_range()
        {
            Mock<IZipCodeRepository> mockZipcodeRepository = new Mock<IZipCodeRepository>();
            ZipCode zipCode = new ZipCode()
            {
                Zip = "07035",
                City = "Lincoln Park",
                State = new State() { Abbreviation = "NJ" }
            };
            List<ZipCode> zipCodeList = new List<ZipCode> { zipCode };
            mockZipcodeRepository.Setup(obj=>obj.GetByZip("07035")).Returns(zipCode);
            mockZipcodeRepository.Setup(obj => obj.GetZipsForRange(zipCode, 5)).Returns(zipCodeList);
            IGeoService geoService = new GeoManager(mockZipcodeRepository.Object);
            List<ZipCodeData> data = geoService.GetZips("07035", 5).ToList();
            Assert.IsTrue(data[0].City.ToUpper() == "LINCOLN PARK");



        }

    }
}
