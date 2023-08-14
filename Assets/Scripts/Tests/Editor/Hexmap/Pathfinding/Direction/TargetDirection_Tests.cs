using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PathfindingTests.DirectionTests
{
    public class TargetDirection_Tests
    {
        Vector3Int startCoords;
        Vector3Int targetCoordsX;
        Vector3Int targetCoordsY;
        Vector3Int targetCoordsZ;

        [SetUp]
        public void SetUp()
        {
            startCoords = Vector3Int.zero;
            targetCoordsX = new Vector3Int(0, -1, 1);
            targetCoordsY = new Vector3Int(1, 0, -1);
            targetCoordsZ = new Vector3Int(1, -1, 0);
        }


        // Test get target direction up
        [Test]
        public void GetTargetDirectionX_Up()
        {
            bool targetUp = Direction.IsTargetUp(startCoords, targetCoordsX);
            Assert.IsTrue(targetUp);
        }


        // Test get target direction down
        [Test]
        public void GetTargetDirectionX_Down()
        {
            bool targetUp = Direction.IsTargetUp(startCoords, -targetCoordsX);
            Assert.IsFalse(targetUp);
        }


        // Test get target direction up
        [Test]
        public void GetTargetDirectionY_Up()
        {
            bool targetUp = Direction.IsTargetUp(startCoords, -targetCoordsY);
            Assert.IsTrue(targetUp);
        }


        // Test get target direction down
        [Test]
        public void GetTargetDirectionY_Down()
        {
            bool targetUp = Direction.IsTargetUp(startCoords, targetCoordsY);
            Assert.IsFalse(targetUp);
        }


        // Test get target direction up
        [Test]
        public void GetTargetDirectionZ_Up()
        {
            bool targetUp = Direction.IsTargetUp(startCoords, targetCoordsZ);
            Assert.IsTrue(targetUp);
        }


        // Test get target direction down
        [Test]
        public void GetTargetDirectionZ_Down()
        {
            bool targetUp = Direction.IsTargetUp(startCoords, -targetCoordsZ);
            Assert.IsFalse(targetUp);
        }


        // Test get target direction up
        [Test]
        public void GetTargetDirectionX_Up_2()
        {
            bool targetUp = Direction.IsTargetUp(targetCoordsX, startCoords);
            Assert.IsFalse(targetUp);
        }


        // Test get target direction down
        [Test]
        public void GetTargetDirectionX_Down_2()
        {
            bool targetUp = Direction.IsTargetUp(-targetCoordsX, startCoords);
            Assert.IsTrue(targetUp);
        }


        // Test get target direction up
        [Test]
        public void GetTargetDirectionY_Up_2()
        {
            bool targetUp = Direction.IsTargetUp(targetCoordsY, startCoords);
            Assert.IsTrue(targetUp);
        }


        // Test get target direction down
        [Test]
        public void GetTargetDirectionY_Down_2()
        {
            bool targetUp = Direction.IsTargetUp(-targetCoordsY, startCoords);
            Assert.IsFalse(targetUp);
        }


        // Test get target direction up
        [Test]
        public void GetTargetDirectionZ_Up_2()
        {
            bool targetUp = Direction.IsTargetUp(targetCoordsZ, startCoords);
            Assert.IsFalse(targetUp);
        }


        // Test get target direction down
        [Test]
        public void GetTargetDirectionZ_Down_2()
        {
            bool targetUp = Direction.IsTargetUp(-targetCoordsZ, startCoords);
            Assert.IsTrue(targetUp);
        }
    }
}