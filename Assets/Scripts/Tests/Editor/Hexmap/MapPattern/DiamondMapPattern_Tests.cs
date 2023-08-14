using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace HexmapTests.PatternTests
{
    public class DiamondMapPattern_Tests
    {
        private DiamondMapPattern diamondMap;


        // Test diamond pattern 
        [Test]
        public void CreatesDiamondPattern_Width2_Height2()
        {
            diamondMap = new DiamondMapPattern(2, 2);
            diamondMap.PrintMapPattern();
            Assert.AreEqual(4, diamondMap.numHexes);
        }


        // Test diamond pattern 
        [Test]
        public void CreatesDiamondPattern_Width2_Height3()
        {
            diamondMap = new DiamondMapPattern(2, 3);
            diamondMap.PrintMapPattern();
            Assert.AreEqual(6, diamondMap.numHexes);
        }


        // Test diamond pattern 
        [Test]
        public void CreatesDiamondPattern_Width3_Height2()
        {
            diamondMap = new DiamondMapPattern(3, 2);
            diamondMap.PrintMapPattern();
            Assert.AreEqual(6, diamondMap.numHexes);
        }


        // Test diamond pattern 
        [Test]
        public void CreatesDiamondPattern_Width3_Height3()
        {
            diamondMap = new DiamondMapPattern(3, 3);
            diamondMap.PrintMapPattern();
            Assert.AreEqual(9, diamondMap.numHexes);
        }


        // Test diamond pattern 
        [Test]
        public void CreatesDiamondPattern_Width4_Height3()
        {
            diamondMap = new DiamondMapPattern(4, 3);
            diamondMap.PrintMapPattern();
            Assert.AreEqual(12, diamondMap.numHexes);
        }


        // Test diamond pattern 
        [Test]
        public void CreatesDiamondPattern_Width2_Height2_SlopeDown()
        {
            diamondMap = new DiamondMapPattern(2, 2, false);
            diamondMap.PrintMapPattern();
            Assert.AreEqual(4, diamondMap.numHexes);
        }


        // Test diamond pattern 
        [Test]
        public void CreatesDiamondPattern_Width2_Height3_SlopeDown()
        {
            diamondMap = new DiamondMapPattern(2, 3, false);
            diamondMap.PrintMapPattern();
            Assert.AreEqual(6, diamondMap.numHexes);
        }


        // Test diamond pattern 
        [Test]
        public void CreatesDiamondPattern_Width3_Height2_SlopeDown()
        {
            diamondMap = new DiamondMapPattern(3, 2, false);
            diamondMap.PrintMapPattern();
            Assert.AreEqual(6, diamondMap.numHexes);
        }


        // Test diamond pattern 
        [Test]
        public void CreatesDiamondPattern_Width3_Height3_SlopeDown()
        {
            diamondMap = new DiamondMapPattern(3, 3, false);
            diamondMap.PrintMapPattern();
            Assert.AreEqual(9, diamondMap.numHexes);
        }


        // Test diamond pattern 
        [Test]
        public void CreatesDiamondPattern_Width4_Height3_SlopeDown()
        {
            diamondMap = new DiamondMapPattern(4, 3, false);
            diamondMap.PrintMapPattern();
            Assert.AreEqual(12, diamondMap.numHexes);
        }


        // Test diamond size
        [Test]
        public void CreatesDiamondPattern_CorrectSize_Width3_Height3()
        {
            diamondMap = new DiamondMapPattern(3, 3);
            Assert.AreEqual(80, diamondMap.mapPixelWidth);
            Assert.AreEqual(128, diamondMap.mapPixelHeight);
        }


        // Test diamond size
        [Test]
        public void CreatesDiamondPattern_CorrectSize_Width3_Height4()
        {
            diamondMap = new DiamondMapPattern(3, 4);
            Assert.AreEqual(80, diamondMap.mapPixelWidth);
            Assert.AreEqual(160, diamondMap.mapPixelHeight);
        }


        // Test diamond size
        [Test]
        public void CreatesDiamondPattern_CorrectSize_Width4_Height3()
        {
            diamondMap = new DiamondMapPattern(4, 3);
            Assert.AreEqual(104, diamondMap.mapPixelWidth);
            Assert.AreEqual(144, diamondMap.mapPixelHeight);
        }
    }
}