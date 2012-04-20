using CodingBestPractice;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CodingBestPractice.Test
{
    /// <summary>
    ///This is a test class for DinnersControllerTest and is intended
    ///to contain all DinnersControllerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DinnersControllerTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Details
        ///</summary>
        [TestMethod()]
        public void DetailsTest()
        {
            // Arrange
            var controller = CreateDinnersController();
            // Act
            var result = controller.Details("9");
            // Assert
            Assert.IsInstanceOfType(result.Model, typeof(Dinner));
            Dinner d = result.Model as Dinner;
            Assert.AreEqual(d.Name, "Sample Dinner9");
        }

        private List<Dinner> CreateTestDinners()
        {
            List<Dinner> dinners = new List<Dinner>();
            for (int i = 0; i < 101; i++)
            {
                Dinner sampleDinner = new Dinner()
                {
                    Id = i.ToString(),
                    Name = "Sample Dinner" + i.ToString(),
                    Location = "Sample Dinner Location" + i.ToString()
                };

                dinners.Add(sampleDinner);
            }
            return dinners;
        }

        private BetterDinnersController CreateDinnersController()
        {
            var repository = new FakeDinnerRepository(CreateTestDinners());
            return new BetterDinnersController(repository);
        }
    }
}
