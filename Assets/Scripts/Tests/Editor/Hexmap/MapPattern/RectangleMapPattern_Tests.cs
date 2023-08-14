using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace HexmapTests.PatternTests
{
    public class RectangleMapPattern_Tests
    {
        private RectangleMapPattern rectangleMap;


        // Test rectangle pattern
        [Test]
        public void CreatesRectanglePattern_Width3_Height3()
        {
            rectangleMap = new RectangleMapPattern(3, 3);
            rectangleMap.PrintMapPattern();
            Assert.AreEqual(7, rectangleMap.numHexes);
        }


        // Test rectangle pattern
        [Test]
        public void CreatesRectanglePattern_Width3_Height5()
        {
            rectangleMap = new RectangleMapPattern(3, 5);
            rectangleMap.PrintMapPattern();
            Assert.AreEqual(13, rectangleMap.numHexes);
        }


        // Test rectangle pattern
        [Test]
        public void CreatesRectanglePattern_Width5_Height3()
        {
            rectangleMap = new RectangleMapPattern(5, 3);
            rectangleMap.PrintMapPattern();
            Assert.AreEqual(13, rectangleMap.numHexes);
        }


        // Test rectangle pattern
        [Test]
        public void CreatesRectanglePattern_Width5_Height5()
        {
            rectangleMap = new RectangleMapPattern(5, 5);
            rectangleMap.PrintMapPattern();
            Assert.AreEqual(23, rectangleMap.numHexes);
        }


        // Test rectangle pattern
        [Test]
        public void CreatesRectanglePattern_Width3_Height7()
        {
            rectangleMap = new RectangleMapPattern(3, 7);
            rectangleMap.PrintMapPattern();
            Assert.AreEqual(19, rectangleMap.numHexes);
        }


        // Test rectangle pattern
        [Test]
        public void CreatesRectanglePattern_Width7_Height3()
        {
            rectangleMap = new RectangleMapPattern(7, 3);
            rectangleMap.PrintMapPattern();
            Assert.AreEqual(17, rectangleMap.numHexes);
        }


        // Test rectangle pattern
        [Test]
        public void CreatesRectanglePattern_Width3_Height2()
        {
            rectangleMap = new RectangleMapPattern(3, 2);
            rectangleMap.PrintMapPattern();
            Assert.AreEqual(4, rectangleMap.numHexes);
        }


        // Test rectangle pattern
        [Test]
        public void CreatesRectanglePattern_Width5_Height2()
        {
            rectangleMap = new RectangleMapPattern(5, 2);
            rectangleMap.PrintMapPattern();
            Assert.AreEqual(8, rectangleMap.numHexes);
        }


        // Test rectangle pattern
        [Test]
        public void CreatesRectanglePattern_Width4_Height4()
        {
            rectangleMap = new RectangleMapPattern(5, 4);
            rectangleMap.PrintMapPattern();
            Assert.AreEqual(18, rectangleMap.numHexes);
        }


        // Test rectangle size
        [Test]
        public void CreatesRectanglePattern_CorrectSize_Width3_Height3()
        {
            rectangleMap = new RectangleMapPattern(3, 3);
            Assert.AreEqual(80, rectangleMap.mapPixelWidth);
            Assert.AreEqual(96, rectangleMap.mapPixelHeight);
        }


        // Test rectangle size
        [Test]
        public void CreatesRectanglePattern_CorrectSize_Width4_Height3()
        {
            rectangleMap = new RectangleMapPattern(4, 3);
            Assert.AreEqual(104, rectangleMap.mapPixelWidth);
            Assert.AreEqual(96, rectangleMap.mapPixelHeight);
        }


        // Test rectangle size
        [Test]
        public void CreatesRectanglePattern_CorrectSize_Width3_Height4()
        {
            rectangleMap = new RectangleMapPattern(3, 4);
            Assert.AreEqual(80, rectangleMap.mapPixelWidth);
            Assert.AreEqual(128, rectangleMap.mapPixelHeight);
        }
    }
}