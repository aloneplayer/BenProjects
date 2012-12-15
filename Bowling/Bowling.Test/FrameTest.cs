using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bowling.Test
{
    [TestClass]
    public class FrameTest
    {
        [TestMethod]
        public void TestScoreNoThrows()
        {
            Frame f = new Frame();
            Assert.AreEqual(0, f.Score);
        }

        [TestMethod]
        public void TestAddOneThrow()
        {
            Frame f = new Frame();
            f.Add(5);
            Assert.AreEqual(5, f.Score);
        }
    }
}
