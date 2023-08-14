using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PathfindingTests.DirectionTests
{
    public class WallDirection_Tests
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


        // Test X wall direction
        [Test]
        public void GetWallDirectionX_R()
        {
            Vector3Int direction1;
            Vector3Int direction2;
            (direction1, direction2) = Direction.GetWallDirection(startCoords, targetCoordsXDiag);
            Vector3Int expectedDirection1 = Direction.U;
            Vector3Int expectedDirection2 = Direction.D;
            Assert.AreEqual(expectedDirection1, direction1);
            Assert.AreEqual(expectedDirection2, direction2);
        }

        [Test]
        public void GetWallDirectionX_L()
        {
            targetCoordsXDiag = -targetCoordsXDiag;
            Vector3Int direction1;
            Vector3Int direction2;
            (direction1, direction2) = Direction.GetWallDirection(startCoords, targetCoordsXDiag);
            Vector3Int expectedDirection1 = Direction.U;
            Vector3Int expectedDirection2 = Direction.D;
            Assert.AreEqual(expectedDirection1, direction1);
            Assert.AreEqual(expectedDirection2, direction2);
        }

        [Test]
        public void GetWallDirectionX_NoWall()
        {
            Vector3Int direction1;
            Vector3Int direction2;
            (direction1, direction2) = Direction.GetWallDirection(startCoords, targetCoordsNoLine);
            Vector3Int expectedDirection1 = Vector3Int.zero;
            Vector3Int expectedDirection2 = Vector3Int.zero;
            Assert.AreEqual(expectedDirection1, direction1);
            Assert.AreEqual(expectedDirection2, direction2);
        }


        // Test Y wall direction
        [Test]
        public void GetWallDirectionY_DR()
        {
            Vector3Int direction1;
            Vector3Int direction2;
            (direction1, direction2) = Direction.GetWallDirection(startCoords, targetCoordsYDiag);
            Vector3Int expectedDirection1 = Direction.UR;
            Vector3Int expectedDirection2 = Direction.DL;
            Assert.AreEqual(expectedDirection1, direction1);
            Assert.AreEqual(expectedDirection2, direction2);
        }

        [Test]
        public void GetWallDirectionY_UL()
        {
            targetCoordsYDiag = -targetCoordsYDiag;
            Vector3Int direction1;
            Vector3Int direction2;
            (direction1, direction2) = Direction.GetWallDirection(startCoords, targetCoordsYDiag);
            Vector3Int expectedDirection1 = Direction.UR;
            Vector3Int expectedDirection2 = Direction.DL;
            Assert.AreEqual(expectedDirection1, direction1);
            Assert.AreEqual(expectedDirection2, direction2);
        }

        [Test]
        public void GetWallDirectionY_NoWall()
        {
            Vector3Int direction1;
            Vector3Int direction2;
            (direction1, direction2) = Direction.GetWallDirection(startCoords, targetCoordsNoLine);
            Vector3Int expectedDirection1 = Vector3Int.zero;
            Vector3Int expectedDirection2 = Vector3Int.zero;
            Assert.AreEqual(expectedDirection1, direction1);
            Assert.AreEqual(expectedDirection2, direction2);
        }


        // Test Z wall direction
        [Test]
        public void GetWallDirectionZ_UR()
        {
            Vector3Int direction1;
            Vector3Int direction2;
            (direction1, direction2) = Direction.GetWallDirection(startCoords, targetCoordsZDiag);
            Vector3Int expectedDirection1 = Direction.UL;
            Vector3Int expectedDirection2 = Direction.DR;
            Assert.AreEqual(expectedDirection1, direction1);
            Assert.AreEqual(expectedDirection2, direction2);
        }

        [Test]
        public void GetWallDirectionZ_DL()
        {
            targetCoordsZDiag = -targetCoordsZDiag;
            Vector3Int direction1;
            Vector3Int direction2;
            (direction1, direction2) = Direction.GetWallDirection(startCoords, targetCoordsZDiag);
            Vector3Int expectedDirection1 = Direction.UL;
            Vector3Int expectedDirection2 = Direction.DR;
            Assert.AreEqual(expectedDirection1, direction1);
            Assert.AreEqual(expectedDirection2, direction2);
        }

        [Test]
        public void GetWallDirectionZ_NoWall()
        {
            Vector3Int direction1;
            Vector3Int direction2;
            (direction1, direction2) = Direction.GetWallDirection(startCoords, targetCoordsNoLine);
            Vector3Int expectedDirection1 = Vector3Int.zero;
            Vector3Int expectedDirection2 = Vector3Int.zero;
            Assert.AreEqual(expectedDirection1, direction1);
            Assert.AreEqual(expectedDirection2, direction2);
        }
    }
}