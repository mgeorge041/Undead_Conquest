using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayerTests
{
    public class PlayerStartPieces_Tests
    {
        private PlayerStartPieces startPieces;


        // Setup
        [SetUp]
        public void Setup()
        {
            startPieces = PlayerStartPieces.LoadStartPieces(StartPiecesPaths.testStartPieces);
        }


        // Test creates player start pieces
        [Test]
        public void CreatesPlayerStartPieces()
        {
            Assert.IsNotNull(startPieces);
        }


        // Test sets info
        [Test]
        public void SetsStartPiecesInfo()
        {
            Assert.AreEqual(1, startPieces.pieces.Count);
        }
    }
}