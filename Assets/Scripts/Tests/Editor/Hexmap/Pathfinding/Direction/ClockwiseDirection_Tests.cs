using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PathfindingTests.DirectionTests
{
    public class ClockwiseDirection_Tests
    {
        // Test get next clockwise direction
        [Test]
        public void GetsNextClockwiseDirection_1Rotation_From0_To1()
        {
            Vector3Int nextDirection = Direction.GetNextClockwiseDirection(Direction.U);
            Assert.AreEqual(Direction.UR, nextDirection);
        }

        [Test]
        public void GetsNextClockwiseDirection_1Rotation_From5_To0()
        {
            Vector3Int nextDirection = Direction.GetNextClockwiseDirection(Direction.UL);
            Assert.AreEqual(Direction.U, nextDirection);
        }

        [Test]
        public void GetsNextClockwiseDirection_2Rotations_From0_To2()
        {
            Vector3Int nextDirection = Direction.GetNextClockwiseDirection(Direction.U, 2);
            Assert.AreEqual(Direction.DR, nextDirection);
        }

        [Test]
        public void GetsNextClockwiseDirection_2Rotations_From5_To1()
        {
            Vector3Int nextDirection = Direction.GetNextClockwiseDirection(Direction.UL, 2);
            Assert.AreEqual(Direction.UR, nextDirection);
        }


        // Test get next counter-clockwise direction
        [Test]
        public void GetsNextCounterClockwiseDirection_1Rotation_From0_To5()
        {
            Vector3Int nextDirection = Direction.GetNextCounterClockwiseDirection(Direction.U);
            Assert.AreEqual(Direction.UL, nextDirection);
        }

        [Test]
        public void GetsNextCounterClockwiseDirection_1Rotation_From1_To0()
        {
            Vector3Int nextDirection = Direction.GetNextCounterClockwiseDirection(Direction.UR);
            Assert.AreEqual(Direction.U, nextDirection);
        }

        [Test]
        public void GetsNextCounterClockwiseDirection_2Rotations_From0_To4()
        {
            Vector3Int nextDirection = Direction.GetNextCounterClockwiseDirection(Direction.U, 2);
            Assert.AreEqual(Direction.DL, nextDirection);
        }

        [Test]
        public void GetsNextCounterClockwiseDirection_2Rotations_From1_To5()
        {
            Vector3Int nextDirection = Direction.GetNextCounterClockwiseDirection(Direction.UR, 2);
            Assert.AreEqual(Direction.UL, nextDirection);
        }
    }
}