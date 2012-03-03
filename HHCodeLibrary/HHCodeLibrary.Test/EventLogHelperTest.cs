using HHCodeLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace HHCodeLibrary.Test
{
    
    
    /// <summary>
    ///This is a test class for EventLogHelperTest and is intended
    ///to contain all EventLogHelperTest Unit Tests
    ///</summary>
    [TestClass()]
    public class EventLogHelperTest
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
        ///A test for WriteCustomEvent
        ///</summary>
        [TestMethod()]
        public void WriteCustomEventTest()
        {
            string logSource = "HHTest"; // TODO: Initialize to an appropriate value
            string logName = "HHLog"; // TODO: Initialize to an appropriate value
            string message = "Hello Event Log"; // TODO: Initialize to an appropriate value
            EventLogEntryType logEntryType = EventLogEntryType.SuccessAudit;
            EventLogHelper.WriteCustomEvent(logSource, logName, message, logEntryType);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
