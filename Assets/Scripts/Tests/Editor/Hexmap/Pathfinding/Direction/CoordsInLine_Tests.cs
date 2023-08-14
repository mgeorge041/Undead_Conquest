using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PathfindingTests.DirectionTests
{

    public class CoordsInLine_Tests
    {
        Vector3Int startCoords;
        Vector3Int targetCoordsX;
        Vector3Int targetCoordsY;
        Vector3Int targetCoordsZ;
        Vector3Int targetCoordsXDiag;
        Vector3Int targetCoordsYDiag;
        Vector3Int targetCoordsZDiag;
        Vector3Int targetCoordsNoLine;
        bool inLine;

        [SetUp]
        public void SetUp()
        {
            startCoords = Vector3Int.zero;
            targetCoordsX = new Vector3Int(0, -1, 1);
            targetCoordsY = new Vector3Int(1, 0, -1);
            targetCoordsZ = new Vector3Int(1, -1, 0);
            targetCoordsXDiag = new Vector3Int(2, -1, -1);
            targetCoordsYDiag = new Vector3Int(1, 1, -2);
            targetCoordsZDiag = new Vector3Int(-1, 2, -1);
            targetCoordsNoLine = new Vector3Int(4, -3, -1);
        }


        // Test check coords in X line
        [Test]
        public void CoordsInXLine()
        {
            inLine = Direction.CoordsInXLine(startCoords, targetCoordsX);
            Assert.IsTrue(inLine);
            
            inLine = Direction.CoordsInXLine(startCoords, targetCoordsXDiag);
            Assert.IsFalse(inLine);

            inLine = Direction.CoordsInXLine(startCoords, targetCoordsNoLine);
            Assert.IsFalse(inLine);
        }


        // Test check coords in Y line
        [Test]
        public void CoordsInYLine()
        {
            inLine = Direction.CoordsInYLine(startCoords, targetCoordsY);
            Assert.IsTrue(inLine);

            inLine = Direction.CoordsInYLine(startCoords, targetCoordsYDiag);
            Assert.IsFalse(inLine);

            inLine = Direction.CoordsInYLine(startCoords, targetCoordsNoLine);
            Assert.IsFalse(inLine);
        }


        // Test check coords in Z line
        [Test]
        public void CoordsInZLine()
        {
            inLine = Direction.CoordsInZLine(startCoords, targetCoordsZ);
            Assert.IsTrue(inLine);

            inLine = Direction.CoordsInZLine(startCoords, targetCoordsZDiag);
            Assert.IsFalse(inLine);

            inLine = Direction.CoordsInZLine(startCoords, targetCoordsNoLine);
            Assert.IsFalse(inLine);
        }


        // Test check coords in line
        [Test]
        public void CoordsInLine()
        {
            inLine = Direction.CoordsInLine(startCoords, targetCoordsX);
            Assert.IsTrue(inLine);

            inLine = Direction.CoordsInLine(targetCoordsX, targetCoordsZ);
            Assert.IsTrue(inLine);

            inLine = Direction.CoordsInLine(targetCoordsX, targetCoordsY);
            Assert.IsFalse(inLine);

            inLine = Direction.CoordsInLine(targetCoordsY, targetCoordsZ);
            Assert.IsTrue(inLine);
        }


        // Test check coords in X diagonal
        [Test]
        public void CoordsInXDiagonal()
        {
            inLine = Direction.CoordsInXDiagonal(startCoords, targetCoordsXDiag);
            Assert.IsTrue(inLine);

            inLine = Direction.CoordsInXDiagonal(startCoords, targetCoordsX);
            Assert.IsFalse(inLine);

            inLine = Direction.CoordsInXDiagonal(startCoords, targetCoordsNoLine);
            Assert.IsFalse(inLine);
        }


        // Test check coords in Y diagonal
        [Test]
        public void CoordsInYDiagonal()
        {
            inLine = Direction.CoordsInYDiagonal(startCoords, targetCoordsYDiag);
            Assert.IsTrue(inLine);

            inLine = Direction.CoordsInYDiagonal(startCoords, targetCoordsY);
            Assert.IsFalse(inLine);

            inLine = Direction.CoordsInYDiagonal(startCoords, targetCoordsNoLine);
            Assert.IsFalse(inLine);
        }


        // Test check coords in Z diagonal
        [Test]
        public void CoordsInZDiagonal()
        {
            inLine = Direction.CoordsInZDiagonal(startCoords, targetCoordsZDiag);
            Assert.IsTrue(inLine);

            inLine = Direction.CoordsInZDiagonal(startCoords, targetCoordsZ);
            Assert.IsFalse(inLine);

            inLine = Direction.CoordsInZDiagonal(startCoords, targetCoordsNoLine);
            Assert.IsFalse(inLine);
        }


        // Test check coords in diagonal
        [Test]
        public void CoordsInDiagonal()
        {
            inLine = Direction.CoordsInDiagonalLine(startCoords, targetCoordsX);
            Assert.IsFalse(inLine);

            inLine = Direction.CoordsInDiagonalLine(targetCoordsX, targetCoordsZ);
            Assert.IsFalse(inLine);

            inLine = Direction.CoordsInDiagonalLine(targetCoordsX, targetCoordsY);
            Assert.IsTrue(inLine);

            inLine = Direction.CoordsInDiagonalLine(targetCoordsY, targetCoordsZ);
            Assert.IsFalse(inLine);
        }
    }
}