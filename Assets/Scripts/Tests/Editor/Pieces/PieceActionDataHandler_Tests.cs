using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PieceTests
{
    public class PieceActionDataHandler_Tests
    {
        private PieceActionDataHandler dataHandler;


        // Setup
        [SetUp]
        public void Setup()
        {
            dataHandler = new PieceActionDataHandler();
        }


        // Test creates piece action data handler
        [Test]
        public void CreatesPieceActionDataHandler()
        {
            Assert.IsNotNull(dataHandler);
        }


        // Test queueing piece action data
        [Test]
        public void QueuesPieceActionData()
        {
            Unit unit = Unit.CreateUnit(CardPaths.testUnit);
            HexmapData hexmapData = new HexmapData();
            hexmapData.AddPiece(unit, Vector3Int.zero);

            Vector3Int targetHexCoords = new Vector3Int(1, -1, 0);
            Hex targetHex = hexmapData.GetHexAtHexCoords(targetHexCoords);
            PieceActionData actionData = new PieceActionData(unit, PieceActionType.Move, unit.hex, targetHex);
            dataHandler.QueuePieceActionData(actionData);

            Assert.AreEqual(actionData, dataHandler.actionsQueue.Peek());
        }


        // Test performing actions
        [Test]
        public void PerformsQueuedActions_1Action()
        {
            Unit unit = Unit.CreateUnit(CardPaths.testUnit);
            HexmapData hexmapData = new HexmapData();
            hexmapData.AddPiece(unit, Vector3Int.zero);

            Vector3Int targetHexCoords = new Vector3Int(1, -1, 0);
            Hex startHex = hexmapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexmapData.GetHexAtHexCoords(targetHexCoords);
            PieceActionData actionData = new PieceActionData(unit, PieceActionType.Move, startHex, targetHex);
            dataHandler.QueuePieceActionData(actionData);
            dataHandler.PerformAllQueuedActions();

            Assert.AreEqual(targetHex, unit.hex);
            Assert.AreEqual(unit, targetHex.piece);
            Assert.IsNull(startHex.piece);
        }

        [Test]
        public void PerformsQueuedActions_2Actions()
        {
            Unit unit = Unit.CreateUnit(CardPaths.testUnit);
            HexmapData hexmapData = new HexmapData();
            hexmapData.AddPiece(unit, Vector3Int.zero);

            Vector3Int targetHexCoords = new Vector3Int(1, -1, 0);
            Vector3Int targetHexCoords2 = new Vector3Int(2, -2, 0);
            Hex startHex = hexmapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexmapData.GetHexAtHexCoords(targetHexCoords);
            Hex targetHex2 = hexmapData.GetHexAtHexCoords(targetHexCoords2);
            PieceActionData actionData = new PieceActionData(unit, PieceActionType.Move, startHex, targetHex);
            PieceActionData actionData2 = new PieceActionData(unit, PieceActionType.Move, targetHex, targetHex2);
            dataHandler.QueuePieceActionData(actionData);
            dataHandler.QueuePieceActionData(actionData2);
            dataHandler.PerformAllQueuedActions();

            Assert.AreEqual(targetHex2, unit.hex);
            Assert.AreEqual(unit, targetHex2.piece);
            Assert.IsNull(startHex.piece);
            Assert.IsNull(targetHex.piece);
        }
    }
}