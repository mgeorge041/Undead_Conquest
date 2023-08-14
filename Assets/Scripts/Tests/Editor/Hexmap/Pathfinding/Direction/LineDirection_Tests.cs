using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PathfindingTests.DirectionTests
{
    public class LineDirection_Tests
    {
        Vector3Int startCoords;
        Vector3Int targetCoordsX;
        Vector3Int targetCoordsY;
        Vector3Int targetCoordsZ;
        Vector3Int targetCoordsNoLine;

        [SetUp]
        public void SetUp()
        {
            startCoords = Vector3Int.zero;
            targetCoordsX = new Vector3Int(0, -1, 1);
            targetCoordsY = new Vector3Int(1, 0, -1);
            targetCoordsZ = new Vector3Int(1, -1, 0);
            targetCoordsNoLine = new Vector3Int(4, -3, -1);
        }


        // Test X line direction
        [Test]
        public void GetsLineDirectionX_Up()
        {
            Vector3Int direction = Direction.GetLineDirection(startCoords, targetCoordsX);
            Vector3Int expectedDirection = Direction.U;
            Assert.AreEqual(expectedDirection, direction);
        }

        [Test]
        public void GetsLineDirectionX_Down()
        {
            targetCoordsX = -targetCoordsX;
            Vector3Int direction = Direction.GetLineDirection(startCoords, targetCoordsX);
            Vector3Int expectedDirection = Direction.D;
            Assert.AreEqual(expectedDirection, direction);
        }

        [Test]
        public void GetsLineDirectionX_NoLine()
        {
            Vector3Int direction = Direction.GetLineDirection(startCoords, targetCoordsNoLine);
            Vector3Int expectedDirection = Vector3Int.zero;
            Assert.AreEqual(expectedDirection, direction);
        }


        // Test Y line direction
        [Test]
        public void GetsLineDirectionY_DR()
        {
            Vector3Int direction = Direction.GetLineDirection(startCoords, targetCoordsY);
            Vector3Int expectedDirection = Direction.DR;
            Assert.AreEqual(expectedDirection, direction);
        }

        [Test]
        public void GetsLineDirectionY_UL()
        {
            targetCoordsY = -targetCoordsY;
            Vector3Int direction = Direction.GetLineDirection(startCoords, targetCoordsY);
            Vector3Int expectedDirection = Direction.UL;
            Assert.AreEqual(expectedDirection, direction);
        }

        [Test]
        public void GetsLineDirectionY_NoLine()
        {
            Vector3Int direction = Direction.GetLineDirection(startCoords, targetCoordsNoLine);
            Vector3Int expectedDirection = Vector3Int.zero;
            Assert.AreEqual(expectedDirection, direction);
        }


        // Test Z line direction
        [Test]
        public void GetsLineDirectionZ_UR()
        {
            Vector3Int direction = Direction.GetLineDirection(startCoords, targetCoordsZ);
            Vector3Int expectedDirection = Direction.UR;
            Assert.AreEqual(expectedDirection, direction);
        }

        [Test]
        public void GetsLineDirectionZ_DL()
        {
            targetCoordsZ = -targetCoordsZ;
            Vector3Int direction = Direction.GetLineDirection(startCoords, targetCoordsZ);
            Vector3Int expectedDirection = Direction.DL;
            Assert.AreEqual(expectedDirection, direction);
        }

        [Test]
        public void GetsLineDirectionZ_NoLine()
        {
            Vector3Int direction = Direction.GetLineDirection(startCoords, targetCoordsNoLine);
            Vector3Int expectedDirection = Vector3Int.zero;
            Assert.AreEqual(expectedDirection, direction);
        }
    }
}