using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace HexmapTests.PatternTests
{
    public class HexagonMapPattern_Tests
    {
        private HexagonMapPattern hexagonMap;


        // Setup
        [SetUp]
        public void Setup()
        {
            
        }


        // Test hexagon pattern
        [Test]
        public void CreatesHexagonPattern_Radius1()
        {
            hexagonMap = new HexagonMapPattern(1);
            Assert.AreEqual(7, hexagonMap.numHexes);
        }


        // Test hexagon pattern
        [Test]
        public void CreatesHexagonPattern_Radius2()
        {
            hexagonMap = new HexagonMapPattern(2);
            Assert.AreEqual(19, hexagonMap.numHexes);
        }


        // Test hexagon pattern
        [Test]
        public void CreatesHexagonPattern_Radius3()
        {
            hexagonMap = new HexagonMapPattern(3);
            Assert.AreEqual(37, hexagonMap.numHexes);
        }


        // Test hexagon pattern
        [Test]
        public void CreatesHexagonPattern_Radius0()
        {
            hexagonMap = new HexagonMapPattern(0);
            Assert.AreEqual(1, hexagonMap.numHexes);
        }


        // Test hexagon pattern
        [Test]
        public void DoesNotCreateHexagonPattern_RadiusNegative()
        {
            hexagonMap = new HexagonMapPattern(-1);
            Assert.AreEqual(0, hexagonMap.numHexes);
        }


        // Test hexagon size
        [Test]
        public void CreatesHexagonPattern_CorrectSize_Radius1()
        {
            hexagonMap = new HexagonMapPattern(1);
            Assert.AreEqual(80, hexagonMap.mapPixelWidth);
            Assert.AreEqual(96, hexagonMap.mapPixelHeight);
        }


        // Test hexagon size
        [Test]
        public void CreatesHexagonPattern_CorrectSize_Radius2()
        {
            hexagonMap = new HexagonMapPattern(2);
            Assert.AreEqual(128, hexagonMap.mapPixelWidth);
            Assert.AreEqual(160, hexagonMap.mapPixelHeight);
        }
    }
}