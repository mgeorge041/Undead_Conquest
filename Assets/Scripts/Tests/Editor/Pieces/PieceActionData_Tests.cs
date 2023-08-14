using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PieceTests
{
    public class PieceActionData_Tests
    {
        private PieceActionData data;
        private Piece piece;
        private HexmapData hexmapData;


        // Setup
        [SetUp]
        public void Setup()
        {
            hexmapData = new HexmapData();
            data = new PieceActionData();
        }


        // Test creates piece action data
        [Test]
        public void CreatesPieceActionData()
        {
            Assert.IsNotNull(data);
        }

        [Test]
        public void CreatesPieceActionData_NullPiece_ThrowsError()
        {
            Assert.Throws<System.ArgumentNullException>(() => data = new PieceActionData(null, PieceActionType.Move, new Hex(Vector3Int.zero), new Hex(Vector3Int.zero)));
        }

        [Test]
        public void CreatesPieceActionData_NullPieceHex_ThrowsError()
        {
            piece = Unit.CreateUnit(CardPaths.testUnit);
            Assert.Throws<System.ArgumentNullException>(() => data = new PieceActionData(piece, PieceActionType.Move, new Hex(Vector3Int.zero), new Hex(Vector3Int.zero)));
        }

        [Test]
        public void CreatesPieceActionData_NoneAction_ThrowsError()
        {
            piece = Unit.CreateUnit(CardPaths.testUnit);
            Assert.Throws<System.ArgumentNullException>(() => data = new PieceActionData(piece, PieceActionType.None, new Hex(Vector3Int.zero), new Hex(Vector3Int.zero)));
        }

        [Test]
        public void CreatesPieceActionData_NullStartHex_ThrowsError()
        {
            piece = Unit.CreateUnit(CardPaths.testUnit);
            Assert.Throws<System.ArgumentNullException>(() => data = new PieceActionData(piece, PieceActionType.Move, null, new Hex(Vector3Int.zero)));
        }


        [Test]
        public void CreatesPieceActionData_NullTargetHex_ThrowsError()
        {
            piece = Unit.CreateUnit(CardPaths.testUnit);
            Assert.Throws<System.ArgumentNullException>(() => data = new PieceActionData(piece, PieceActionType.Move, new Hex(Vector3Int.zero), null));
        }


        // Test create move action start data
        [Test]
        public void CreatesPieceActionData_MoveAction()
        {
            piece = Unit.CreateUnit(CardPaths.testUnit);
            hexmapData.AddPiece(piece, Vector3Int.zero);
            Hex startHex = hexmapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexmapData.GetHexAtHexCoords(new Vector3Int(1, -1, 0));
            data = new PieceActionData(piece, PieceActionType.Move, startHex, targetHex);

            Assert.AreEqual(piece, data.actionPiece);
            Assert.AreEqual(piece, data.actionUnit);
            Assert.AreEqual(PieceActionType.Move, data.actionType);
            Assert.AreEqual(startHex, data.startHex);
            Assert.AreEqual(targetHex, data.targetHex);
            Assert.AreEqual(3, data.startUnitData.currentSpeed);
        }


        // Test move unit
        [Test]
        public void PerformsMoveUnit()
        {
            piece = Unit.CreateUnit(CardPaths.testUnit);
            hexmapData.AddPiece(piece, Vector3Int.zero);
            Hex startHex = hexmapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexmapData.GetHexAtHexCoords(new Vector3Int(1, -1, 0));
            data = new PieceActionData(piece, PieceActionType.Move, startHex, targetHex);
            data.PerformAction();

            Assert.IsNull(startHex.piece);
            Assert.AreEqual(targetHex, piece.hex);
            Assert.AreEqual(piece, targetHex.piece);
            Assert.AreEqual(2, data.endUnitData.currentSpeed);
        }


        // Test undo move
        [Test]
        public void UndoesMove()
        {
            piece = Unit.CreateUnit(CardPaths.testUnit);
            hexmapData.AddPiece(piece, Vector3Int.zero);
            Hex startHex = hexmapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexmapData.GetHexAtHexCoords(new Vector3Int(1, -1, 0));
            data = new PieceActionData(piece, PieceActionType.Move, startHex, targetHex);
            data.PerformAction();
            data.UndoAction();

            Assert.AreEqual(piece, startHex.piece);
            Assert.IsNull(targetHex.piece);
            Assert.AreEqual(startHex, piece.hex);
            Assert.AreEqual(3, data.actionUnit.unitData.currentSpeed);
        }


        // Test attack
        [Test]
        public void PerformsAttack()
        {
            piece = Unit.CreateUnit(CardPaths.testUnit);
            Piece enemyPiece = Piece.CreatePiece(CardPaths.testUnit);
            Vector3Int targetHexCoords = new Vector3Int(1, -1, 0);

            hexmapData.AddPiece(piece, Vector3Int.zero);
            hexmapData.AddPiece(enemyPiece, targetHexCoords);

            Hex targetHex = hexmapData.GetHexAtHexCoords(targetHexCoords);
            data = new PieceActionData(piece, PieceActionType.Attack, piece.hex, targetHex);
            data.PerformAction();

            Assert.AreEqual(3, data.endTargetUnitData.currentHealth);
            Assert.IsFalse(data.endUnitData.hasActions);
        }


        // Test undo attack
        [Test]
        public void UndoesAttack()
        {
            piece = Unit.CreateUnit(CardPaths.testUnit);
            Piece enemyPiece = Piece.CreatePiece(CardPaths.testUnit);
            Vector3Int targetHexCoords = new Vector3Int(1, -1, 0);

            hexmapData.AddPiece(piece, Vector3Int.zero);
            hexmapData.AddPiece(enemyPiece, targetHexCoords);

            Hex targetHex = hexmapData.GetHexAtHexCoords(targetHexCoords);
            data = new PieceActionData(piece, PieceActionType.Attack, piece.hex, targetHex);
            data.PerformAction();
            data.UndoAction();

            Assert.AreEqual(5, enemyPiece.pieceData.currentHealth);
            Assert.IsTrue(piece.pieceData.hasActions);
        }
    }
}