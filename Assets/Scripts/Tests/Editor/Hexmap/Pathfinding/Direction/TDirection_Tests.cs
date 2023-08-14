using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PathfindingTests.DirectionTests
{
    public class TDirection_Tests
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


        // Test X T shape direction
        [Test]
        public void GetTDirectionX_U()
        {
            Vector3Int direction1;
            Vector3Int direction2;
            Vector3Int direction3;
            (direction1, direction2, direction3) = Direction.GetTDirection(startCoords, targetCoordsX);
            Vector3Int expectedDirection1 = Direction.UL;
            Vector3Int expectedDirection2 = Direction.U;
            Vector3Int expectedDirection3 = Direction.UR;
            Assert.AreEqual(expectedDirection1, direction1);
            Assert.AreEqual(expectedDirection2, direction2);
            Assert.AreEqual(expectedDirection3, direction3);
        }

        [Test]
        public void GetTDirectionX_D()
        {
            targetCoordsX = -targetCoordsX;
            Vector3Int direction1;
            Vector3Int direction2;
            Vector3Int direction3;
            (direction1, direction2, direction3) = Direction.GetTDirection(startCoords, targetCoordsX);
            Vector3Int expectedDirection1 = Direction.DR;
            Vector3Int expectedDirection2 = Direction.D;
            Vector3Int expectedDirection3 = Direction.DL;
            Assert.AreEqual(expectedDirection1, direction1);
            Assert.AreEqual(expectedDirection2, direction2);
            Assert.AreEqual(expectedDirection3, direction3);
        }

        [Test]
        public void GetTDirectionX_NoT()
        {
            Vector3Int direction1;
            Vector3Int direction2;
            Vector3Int direction3;
            (direction1, direction2, direction3) = Direction.GetTDirection(startCoords, targetCoordsNoLine);
            Vector3Int expectedDirection1 = Vector3Int.zero;
            Vector3Int expectedDirection2 = Vector3Int.zero;
            Vector3Int expectedDirection3 = Vector3Int.zero;
            Assert.AreEqual(expectedDirection1, direction1);
            Assert.AreEqual(expectedDirection2, direction2);
            Assert.AreEqual(expectedDirection3, direction3);
        }


        // Test Y T shape direction
        [Test]
        public void GetTDirectionY_DR()
        {
            Vector3Int direction1;
            Vector3Int direction2;
            Vector3Int direction3;
            (direction1, direction2, direction3) = Direction.GetTDirection(startCoords, targetCoordsY);
            Vector3Int expectedDirection1 = Direction.UR;
            Vector3Int expectedDirection2 = Direction.DR;
            Vector3Int expectedDirection3 = Direction.D;
            Assert.AreEqual(expectedDirection1, direction1);
            Assert.AreEqual(expectedDirection2, direction2);
            Assert.AreEqual(expectedDirection3, direction3);
        }

        [Test]
        public void GetTDirectionY_UL()
        {
            targetCoordsY = -targetCoordsY;
            Vector3Int direction1;
            Vector3Int direction2;
            Vector3Int direction3;
            (direction1, direction2, direction3) = Direction.GetTDirection(startCoords, targetCoordsY);
            Vector3Int expectedDirection1 = Direction.DL;
            Vector3Int expectedDirection2 = Direction.UL;
            Vector3Int expectedDirection3 = Direction.U;
            Assert.AreEqual(expectedDirection1, direction1);
            Assert.AreEqual(expectedDirection2, direction2);
            Assert.AreEqual(expectedDirection3, direction3);
        }

        [Test]
        public void GetTDirectionY_NoT()
        {
            Vector3Int direction1;
            Vector3Int direction2;
            Vector3Int direction3;
            (direction1, direction2, direction3) = Direction.GetTDirection(startCoords, targetCoordsNoLine);
            Vector3Int expectedDirection1 = Vector3Int.zero;
            Vector3Int expectedDirection2 = Vector3Int.zero;
            Vector3Int expectedDirection3 = Vector3Int.zero;
            Assert.AreEqual(expectedDirection1, direction1);
            Assert.AreEqual(expectedDirection2, direction2);
            Assert.AreEqual(expectedDirection3, direction3);
        }


        // Test Z T shape direction
        [Test]
        public void GetTDirectionZ_UR()
        {
            Vector3Int direction1;
            Vector3Int direction2;
            Vector3Int direction3;
            (direction1, direction2, direction3) = Direction.GetTDirection(startCoords, targetCoordsZ);
            Vector3Int expectedDirection1 = Direction.U;
            Vector3Int expectedDirection2 = Direction.UR;
            Vector3Int expectedDirection3 = Direction.DR;
            Assert.AreEqual(expectedDirection1, direction1);
            Assert.AreEqual(expectedDirection2, direction2);
            Assert.AreEqual(expectedDirection3, direction3);
        }

        [Test]
        public void GetTDirectionZ_DL()
        {
            targetCoordsZ = -targetCoordsZ;
            Vector3Int direction1;
            Vector3Int direction2;
            Vector3Int direction3;
            (direction1, direction2, direction3) = Direction.GetTDirection(startCoords, targetCoordsZ);
            Vector3Int expectedDirection1 = Direction.D;
            Vector3Int expectedDirection2 = Direction.DL;
            Vector3Int expectedDirection3 = Direction.UL;
            Assert.AreEqual(expectedDirection1, direction1);
            Assert.AreEqual(expectedDirection2, direction2);
            Assert.AreEqual(expectedDirection3, direction3);
        }

        [Test]
        public void GetTDirectionZ_NoT()
        {
            Vector3Int direction1;
            Vector3Int direction2;
            Vector3Int direction3;
            (direction1, direction2, direction3) = Direction.GetTDirection(startCoords, targetCoordsNoLine);
            Vector3Int expectedDirection1 = Vector3Int.zero;
            Vector3Int expectedDirection2 = Vector3Int.zero;
            Vector3Int expectedDirection3 = Vector3Int.zero;
            Assert.AreEqual(expectedDirection1, direction1);
            Assert.AreEqual(expectedDirection2, direction2);
            Assert.AreEqual(expectedDirection3, direction3);
        }
    }
}