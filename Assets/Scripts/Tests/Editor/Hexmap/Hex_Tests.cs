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
        private int numSetPieceEvents;
        private Piece addPiece;
        private Piece setPiece;


        // Setup
        [SetUp]
        public void Setup()
        {
            hex = new Hex();
            numAddPieceEvents = 0;
            numSetPieceEvents = 0;
            addPiece = null;
            setPiece = null;
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


        // Test adding and setting piece
        [Test]
        public void SetsPiece()
        {
            Unit unit = Unit.CreateUnit(CardPaths.testUnit);
            hex.SetPiece(unit);
            Assert.AreEqual(unit, hex.unit);
        }

        [Test]
        public void AddsNewPiece()
        {
            Unit unit = Unit.CreateUnit(CardPaths.testUnit);
            hex.AddNewPiece(unit);
            Assert.AreEqual(unit, hex.unit);
        }


        // Test adding piece
        private void HandleAddNewPieceEvent(Piece piece)
        {
            numAddPieceEvents++;
            addPiece = piece;
        }
        private void HandleSetPieceEvent(Piece piece)
        {
            numSetPieceEvents++;
            setPiece = piece;
        }

        [Test]
        public void FiresEvent_SetPiece()
        {
            hex.eventManager.onAddPiece.Subscribe(HandleSetPieceEvent);
            Piece piece = Piece.CreatePiece(CardPaths.testUnit);
            hex.AddNewPiece(piece);
            Assert.AreEqual(1, numSetPieceEvents);
            Assert.AreEqual(piece, setPiece);
        }

        [Test]
        public void FiresEvent_AddNewPiece()
        {
            hex.eventManager.onAddPiece.Subscribe(HandleSetPieceEvent);
            hex.eventManager.onAddPiece.Subscribe(HandleAddNewPieceEvent);
            Piece piece = Piece.CreatePiece(CardPaths.testUnit);
            hex.AddNewPiece(piece);
            Assert.AreEqual(1, numAddPieceEvents);
            Assert.AreEqual(piece, addPiece);
            Assert.AreEqual(1, numSetPieceEvents);
            Assert.AreEqual(piece, setPiece);
        }

        
    }
}