using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PathfindingTests.DirectionTests
{
    public class RingDirection_Tests
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


        // Test X ring direction
        [Test]
        public void GetsRingDirectionX_Up()
        {
            Vector3Int direction1;
            Vector3Int direction2;
            (direction1, direction2) = Direction.GetRingDirection(startCoords, targetCoordsX);
            Vector3Int expectedDirection1 = Direction.DR;
            Vector3Int expectedDirection2 = Direction.DL;
            Assert.AreEqual(expectedDirection1, direction1);
            Assert.AreEqual(expectedDirection2, direction2);
        }

        [Test]
        public void GetsRingDirectionX_Down()
        {
            targetCoordsX = -targetCoordsX;
            Vector3Int direction1;
            Vector3Int direction2;
            (direction1, direction2) = Direction.GetRingDirection(startCoords, targetCoordsX);
            Vector3Int expectedDirection1 = Direction.UL;
            Vector3Int expectedDirection2 = Direction.UR;
            Assert.AreEqual(expectedDirection1, direction1);
            Assert.AreEqual(expectedDirection2, direction2);
        }

        [Test]
        public void GetsRingDirectionX_NoRing()
        {
            Vector3Int direction1;
            Vector3Int direction2;
            (direction1, direction2) = Direction.GetRingDirection(startCoords, targetCoordsNoLine);
            Vector3Int expectedDirection1 = Vector3Int.zero;
            Vector3Int expectedDirection2 = Vector3Int.zero;
            Assert.AreEqual(expectedDirection1, direction1);
            Assert.AreEqual(expectedDirection2, direction2);
        }


        // Test Y ring direction
        [Test]
        public void GetsRingDirectionY_DR()
        {
            Vector3Int direction1;
            Vector3Int direction2;
            (direction1, direction2) = Direction.GetRingDirection(startCoords, targetCoordsY);
            Vector3Int expectedDirection1 = Direction.DL;
            Vector3Int expectedDirection2 = Direction.U;
            Assert.AreEqual(expectedDirection1, direction1);
            Assert.AreEqual(expectedDirection2, direction2);
        }

        [Test]
        public void GetsRingDirectionY_UL()
        {
            targetCoordsY = -targetCoordsY;
            Vector3Int direction1;
            Vector3Int direction2;
            (direction1, direction2) = Direction.GetRingDirection(startCoords, targetCoordsY);
            Vector3Int expectedDirection1 = Direction.UR;
            Vector3Int expectedDirection2 = Direction.D;
            Assert.AreEqual(expectedDirection1, direction1);
            Assert.AreEqual(expectedDirection2, direction2);
        }

        [Test]
        public void GetsRingDirectionY_NoRing()
        {
            Vector3Int direction1;
            Vector3Int direction2;
            (direction1, direction2) = Direction.GetRingDirection(startCoords, targetCoordsNoLine);
            Vector3Int expectedDirection1 = Vector3Int.zero;
            Vector3Int expectedDirection2 = Vector3Int.zero;
            Assert.AreEqual(expectedDirection1, direction1);
            Assert.AreEqual(expectedDirection2, direction2);
        }


        // Test Z ring direction
        [Test]
        public void GetsRingDirectionZ_UR()
        {
            Vector3Int direction1;
            Vector3Int direction2;
            (direction1, direction2) = Direction.GetRingDirection(startCoords, targetCoordsZ);
            Vector3Int expectedDirection1 = Direction.D;
            Vector3Int expectedDirection2 = Direction.UL;
            Assert.AreEqual(expectedDirection1, direction1);
            Assert.AreEqual(expectedDirection2, direction2);
        }

        [Test]
        public void GetsRingDirectionZ_DL()
        {
            targetCoordsZ = -targetCoordsZ;
            Vector3Int direction1;
            Vector3Int direction2;
            (direction1, direction2) = Direction.GetRingDirection(startCoords, targetCoordsZ);
            Vector3Int expectedDirection1 = Direction.U;
            Vector3Int expectedDirection2 = Direction.DR;
            Assert.AreEqual(expectedDirection1, direction1);
            Assert.AreEqual(expectedDirection2, direction2);
        }

        [Test]
        public void GetsRingDirectionZ_NoRing()
        {
            Vector3Int direction1;
            Vector3Int direction2;
            (direction1, direction2) = Direction.GetRingDirection(startCoords, targetCoordsNoLine);
            Vector3Int expectedDirection1 = Vector3Int.zero;
            Vector3Int expectedDirection2 = Vector3Int.zero;
            Assert.AreEqual(expectedDirection1, direction1);
            Assert.AreEqual(expectedDirection2, direction2);
        }
    }
}