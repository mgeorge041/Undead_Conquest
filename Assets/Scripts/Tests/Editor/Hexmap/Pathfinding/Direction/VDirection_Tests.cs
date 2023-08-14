using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PathfindingTests.DirectionTests
{
    public class VDirection_Tests
    {
        Vector3Int startCoords;
        Vector3Int targetCoordsXDiag;
        Vector3Int targetCoordsYDiag;
        Vector3Int targetCoordsZDiag;
        Vector3Int targetCoordsNoLine;

        [SetUp]
        public void SetUp()
        {
            startCoords = Vector3Int.zero;
            targetCoordsXDiag = new Vector3Int(2, -1, -1);
            targetCoordsYDiag = new Vector3Int(1, 1, -2);
            targetCoordsZDiag = new Vector3Int(-1, 2, -1);
            targetCoordsNoLine = new Vector3Int(4, -3, -1);
        }


        // Test X V shape direction
        [Test]
        public void GetVDirectionX_R()
        {
            Vector3Int direction1;
            Vector3Int direction2;
            (direction1, direction2) = Direction.GetVDirection(startCoords, targetCoordsXDiag);
            Vector3Int expectedDirection1 = Direction.UR;
            Vector3Int expectedDirection2 = Direction.DR;
            Assert.AreEqual(expectedDirection1, direction1);
            Assert.AreEqual(expectedDirection2, direction2);
        }

        [Test]
        public void GetVDirectionX_L()
        {
            targetCoordsXDiag = -targetCoordsXDiag;
            Vector3Int direction1;
            Vector3Int direction2;
            (direction1, direction2) = Direction.GetVDirection(startCoords, targetCoordsXDiag);
            Vector3Int expectedDirection1 = Direction.DL;
            Vector3Int expectedDirection2 = Direction.UL;
            Assert.AreEqual(expectedDirection1, direction1);
            Assert.AreEqual(expectedDirection2, direction2);
        }

        [Test]
        public void GetVDirectionX_NoV()
        {
            Vector3Int direction1;
            Vector3Int direction2;
            (direction1, direction2) = Direction.GetVDirection(startCoords, targetCoordsNoLine);
            Vector3Int expectedDirection1 = Vector3Int.zero;
            Vector3Int expectedDirection2 = Vector3Int.zero;
            Assert.AreEqual(expectedDirection1, direction1);
            Assert.AreEqual(expectedDirection2, direction2);
        }


        // Test Y V shape direction
        [Test]
        public void GetVDirectionY_R()
        {
            Vector3Int direction1;
            Vector3Int direction2;
            (direction1, direction2) = Direction.GetVDirection(startCoords, targetCoordsYDiag);
            Vector3Int expectedDirection1 = Direction.DR;
            Vector3Int expectedDirection2 = Direction.D;
            Assert.AreEqual(expectedDirection1, direction1);
            Assert.AreEqual(expectedDirection2, direction2);
        }

        [Test]
        public void GetVDirectionY_L()
        {
            targetCoordsYDiag = -targetCoordsYDiag;
            Vector3Int direction1;
            Vector3Int direction2;
            (direction1, direction2) = Direction.GetVDirection(startCoords, targetCoordsYDiag);
            Vector3Int expectedDirection1 = Direction.UL;
            Vector3Int expectedDirection2 = Direction.U;
            Assert.AreEqual(expectedDirection1, direction1);
            Assert.AreEqual(expectedDirection2, direction2);
        }

        [Test]
        public void GetVDirectionY_NoV()
        {
            Vector3Int direction1;
            Vector3Int direction2;
            (direction1, direction2) = Direction.GetVDirection(startCoords, targetCoordsNoLine);
            Vector3Int expectedDirection1 = Vector3Int.zero;
            Vector3Int expectedDirection2 = Vector3Int.zero;
            Assert.AreEqual(expectedDirection1, direction1);
            Assert.AreEqual(expectedDirection2, direction2);
        }


        // Test Z V shape direction
        [Test]
        public void GetVDirectionZ_DL()
        {
            Vector3Int direction1;
            Vector3Int direction2;
            (direction1, direction2) = Direction.GetVDirection(startCoords, targetCoordsZDiag);
            Vector3Int expectedDirection1 = Direction.D;
            Vector3Int expectedDirection2 = Direction.DL;
            Assert.AreEqual(expectedDirection1, direction1);
            Assert.AreEqual(expectedDirection2, direction2);
        }

        [Test]
        public void GetVDirectionZ_UR()
        {
            targetCoordsZDiag = -targetCoordsZDiag;
            Vector3Int direction1;
            Vector3Int direction2;
            (direction1, direction2) = Direction.GetVDirection(startCoords, targetCoordsZDiag);
            Vector3Int expectedDirection1 = Direction.U;
            Vector3Int expectedDirection2 = Direction.UR;
            Assert.AreEqual(expectedDirection1, direction1);
            Assert.AreEqual(expectedDirection2, direction2);
        }

        [Test]
        public void GetVDirectionZ_NoV()
        {
            Vector3Int direction1;
            Vector3Int direction2;
            (direction1, direction2) = Direction.GetVDirection(startCoords, targetCoordsNoLine);
            Vector3Int expectedDirection1 = Vector3Int.zero;
            Vector3Int expectedDirection2 = Vector3Int.zero;
            Assert.AreEqual(expectedDirection1, direction1);
            Assert.AreEqual(expectedDirection2, direction2);
        }
    }
}