using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bowling;

namespace BowlingTests
{
    [TestClass]
    public class BowlingTests
    {
        [TestMethod]
        public void Test_GutterGame()
        {
            var score = new Game("[0|0] [0|0] [0|0] [0|0] [0|0] [0|0] [0|0] [0|0] [0|0] [0|0]").GetScore();
            Assert.AreEqual(score, 0);
        }

        [TestMethod]
        public void Test_AllSinglesGame()
        {
            var score = new Game("[1|1] [1|1] [1|1] [1|1] [1|1] [1|1] [1|1] [1|1] [1|1] [1|1]").GetScore();
            Assert.AreEqual(score, 20);
        }

        [TestMethod]
        public void Test_SimpleSparesGame()
        {
            var score = new Game("[7|3] [5|5] [0|10] [1|1] [0|0] [0|0] [0|0] [0|0] [0|0] [0|0]").GetScore();
            Assert.AreEqual(score, 38);
        }

        [TestMethod]
        public void Test_SingleStrikeGame()
        {
            var score = new Game("[10] [1|1] [0|0] [0|0] [0|0] [0|0] [0|0] [0|0] [0|0] [0|0]").GetScore();
            Assert.AreEqual(score, 14);
        }

        [TestMethod]
        public void Test_MultiStrikeGame()
        {
            var score = new Game("[10] [10] [10] [1|1] [0|0] [0|0] [0|0] [0|0] [0|0] [0|0]").GetScore();
            Assert.AreEqual(score, 65);
        }

        [TestMethod]
        public void Test_LeadUpSpareGame()
        {
            var score = new Game("[0|0] [0|0] [0|0] [0|0] [0|0] [0|0] [0|0] [0|0] [5|5] [5|5|3]").GetScore();
            Assert.AreEqual(score, 28);
        }

        [TestMethod]
        public void Test_LeadUpStrikesGame()
        {
            var score = new Game("[0|0] [0|0] [0|0] [0|0] [0|0] [0|0] [0|0] [10] [10] [10|10|10]").GetScore();
            Assert.AreEqual(score, 90);
        }
        [TestMethod]
        public void Test_AllSparesGame()
        {
            var score = new Game("[5|5] [5|5] [5|5] [5|5] [5|5] [5|5] [5|5] [5|5] [5|5] [5|5|5]").GetScore();
            Assert.AreEqual(score, 150);
        }
        [TestMethod]
        public void Test_AllStrikesGame()
        {
            var score = new Game("[10] [10] [10] [10] [10] [10] [10] [10] [10] [10|10|10]").GetScore();
            Assert.AreEqual(score, 300);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid input format was allowed.")]
        public void Test_InvalidInputFormat()
        {
            new Game("[5|5] [5|5] [5|5] [5|5] [(5)|5] [5|5] [5|5] [5|5] [5|5] [5|5|5]").GetScore();
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid score entry (>10) was allowed.")]
        public void Test_InvalidScoreEntry()
        {
            new Game("[5|5] [5|5] [5|5] [5|5] [5|5] [5|5] [5|5] [5|5] [10] [10|11|10]").GetScore();
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid frame score was allowed for frame.")]
        public void Test_InvalidFrameScore()
        {
            new Game("[5|5] [5|5] [5|5] [5|5] [5|5] [5|5] [5|5] [5|5] [5|6] [5|5|5]").GetScore();
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "More frames than expected (2) were allowed for frame other than the last.")]
        public void Test_InvalidNumberOfFramesInFrame()
        {
            new Game("[5|5] [5|5] [5|5] [5|5] [5|5] [5|5] [5|5] [5|5] [5|5|5] [5|5|5]").GetScore();
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "More frames than expected (3) were allowed for last frame.")]
        public void Test_InvalidNumberOfFramesInLastFrame()
        {
            new Game("[10] [10] [10] [10] [10] [10] [10] [10] [10] [10|10|10|10]").GetScore();
        }
    }
}
