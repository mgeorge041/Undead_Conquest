using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PieceTests
{
    public class Piece_Tests
    {
        private Piece piece;


        // Setup
        [SetUp]
        public void Setup() { }


        // Test creating unit
        [Test]
        public void CreatesUnit()
        {
            piece = Piece.CreatePiece<Unit>(CardPaths.testUnit);
            Assert.IsNotNull(piece);
            Assert.AreEqual(typeof(Unit), piece.GetType());
        }


        // Test creating building
        [Test]
        public void CreatesBuilding()
        {
            piece = Piece.CreatePiece<Building>(CardPaths.testBuilding);
            Assert.IsNotNull(piece);
            Assert.AreEqual(typeof(Building), piece.GetType());
        }
    }
}