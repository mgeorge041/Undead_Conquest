using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace HexmapTests
{
    public class Hex_Tests
    {
        private Hex hex;
        private int numAddPieceEvents;


        // Setup
        [SetUp]
        public void Setup()
        {
            hex = new Hex();
            numAddPieceEvents = 0;
        }


        // Test creates hex
        [Test]
        public void CreatesHex()
        {
            Assert.IsNotNull(hex);
        }


        // Test initial coordinates
        [Test]
        public void SetsInitialHexCoords()
        {
            Vector3Int hexCoords = new Vector3Int(1, -1, 0);
            hex = new Hex(hexCoords);
            Assert.AreEqual(hexCoords, hex.hexCoords);
            Assert.AreEqual(new Vector3Int(0, 1, 0), hex.tileCoords);
        }


        // Test adding piece
        private void HandleAddPieceEvent(Piece piece)
        {
            numAddPieceEvents++;
        }

        [Test]
        public void FiresEvent_AddPiece()
        {
            hex.eventManager.onAddPiece.Subscribe(HandleAddPieceEvent);
            Piece piece = Piece.CreatePiece(CardPaths.testUnit);
            hex.AddPiece(piece);
            Assert.AreEqual(1, numAddPieceEvents);
            Assert.AreEqual(piece, hex.piece);
        }
    }
}