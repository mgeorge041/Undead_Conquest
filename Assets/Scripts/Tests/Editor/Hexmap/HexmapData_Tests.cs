using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace HexmapTests
{
    public class HexmapData_Tests
    {
        private HexmapData hexmapData;


        // Setup
        [SetUp]
        public void Setup()
        {
            hexmapData = new HexmapData();
        }


        // Test creates hexmap data
        [Test]
        public void CreatesHexmapData()
        {
            Assert.IsNotNull(hexmapData);
        }


        // Test creates default hexes
        [Test]
        public void CreatesDefaultHexagonMap()
        {
            Assert.AreEqual(91, hexmapData.numHexes);
            Assert.IsNotNull(hexmapData.GetHexAtHexCoords(Vector3Int.zero));
            Assert.IsNotNull(hexmapData.GetHexAtHexCoords(new Vector3Int(1, -1, 0)));
            Assert.IsNotNull(hexmapData.GetHexAtHexCoords(new Vector3Int(2, -2, 0)));
            Assert.IsNotNull(hexmapData.GetHexAtHexCoords(new Vector3Int(3, -3, 0)));
            Assert.IsNotNull(hexmapData.GetHexAtHexCoords(new Vector3Int(4, -4, 0)));
            Assert.IsNotNull(hexmapData.GetHexAtHexCoords(new Vector3Int(5, -5, 0)));
        }

        [Test]
        public void SetsHexNeighbors_Center()
        {
            HexPathNode centerHexNode = hexmapData.GetHexAtHexCoords(Vector3Int.zero).pathNode;
            HexPathNode neighbor;
            neighbor = centerHexNode.GetNeighbor(Direction.U);
            Assert.IsNotNull(neighbor);
            Assert.AreEqual(Direction.U, neighbor.hexCoords);

            neighbor = centerHexNode.GetNeighbor(Direction.UR);
            Assert.IsNotNull(neighbor);
            Assert.AreEqual(Direction.UR, neighbor.hexCoords);

            neighbor = centerHexNode.GetNeighbor(Direction.DR);
            Assert.IsNotNull(neighbor);
            Assert.AreEqual(Direction.DR, neighbor.hexCoords);

            neighbor = centerHexNode.GetNeighbor(Direction.D);
            Assert.IsNotNull(neighbor);
            Assert.AreEqual(Direction.D, neighbor.hexCoords);

            neighbor = centerHexNode.GetNeighbor(Direction.DL);
            Assert.IsNotNull(neighbor);
            Assert.AreEqual(Direction.DL, neighbor.hexCoords);

            neighbor = centerHexNode.GetNeighbor(Direction.UL);
            Assert.IsNotNull(neighbor);
            Assert.AreEqual(Direction.UL, neighbor.hexCoords);
        }

        [Test]
        public void SetsHexNeighbors_Edge()
        {
            HexPathNode centerHexNode = hexmapData.GetHexAtHexCoords(new Vector3Int(5, -5, 0)).pathNode;
            HexPathNode neighbor;
            Vector3Int neighborCoords;
            neighbor = centerHexNode.GetNeighbor(Direction.U);
            Assert.IsNull(neighbor);

            neighbor = centerHexNode.GetNeighbor(Direction.UR);
            Assert.IsNull(neighbor);

            neighbor = centerHexNode.GetNeighbor(Direction.DR);
            Assert.IsNull(neighbor);

            neighbor = centerHexNode.GetNeighbor(Direction.D);
            Assert.IsNotNull(neighbor);
            neighborCoords = Direction.D + centerHexNode.hexCoords;
            Assert.AreEqual(neighborCoords, neighbor.hexCoords);

            neighbor = centerHexNode.GetNeighbor(Direction.DL);
            Assert.IsNotNull(neighbor);
            neighborCoords = Direction.DL + centerHexNode.hexCoords;
            Assert.AreEqual(neighborCoords, neighbor.hexCoords);

            neighbor = centerHexNode.GetNeighbor(Direction.UL);
            Assert.IsNotNull(neighbor);
            neighborCoords = Direction.UL + centerHexNode.hexCoords;
            Assert.AreEqual(neighborCoords, neighbor.hexCoords);
        }


        // Test setting new map pattern
        [Test]
        public void SetsNewMapPattern()
        {
            MapPattern mapPattern = MapPattern.CreateMapPattern(MapType.Hexagon, 3);
            hexmapData.SetMapPattern(mapPattern);

            Assert.AreEqual(37, hexmapData.numHexes);
            Assert.IsNotNull(hexmapData.GetHexAtHexCoords(Vector3Int.zero));
            Assert.IsNotNull(hexmapData.GetHexAtHexCoords(new Vector3Int(1, -1, 0)));
            Assert.IsNotNull(hexmapData.GetHexAtHexCoords(new Vector3Int(2, -2, 0)));
            Assert.IsNotNull(hexmapData.GetHexAtHexCoords(new Vector3Int(3, -3, 0)));
            Assert.IsNull(hexmapData.GetHexAtHexCoords(new Vector3Int(4, -4, 0)));
            Assert.IsNull(hexmapData.GetHexAtHexCoords(new Vector3Int(5, -5, 0)));
        }


        // Test adding piece
        [Test]
        public void AddsPiece()
        {
            Piece piece = Piece.CreatePiece<Unit>(CardPaths.testUnit);
            Vector3Int hexCoords = Vector3Int.zero;
            Hex hex = hexmapData.GetHexAtHexCoords(hexCoords);
            hexmapData.AddPiece(piece, hexCoords);

            Assert.AreEqual(hex, piece.hex);
            Assert.AreEqual(piece, hex.piece);
            Assert.AreEqual(1, hexmapData.pieces.Count);
        }


        // Test removing piece
        [Test]
        public void RemovesPiece()
        {
            Piece piece = Piece.CreatePiece<Unit>(CardPaths.testUnit);
            Vector3Int hexCoords = Vector3Int.zero;
            Hex hex = hexmapData.GetHexAtHexCoords(hexCoords);
            hexmapData.AddPiece(piece, hexCoords);
            hexmapData.RemovePiece(piece);

            Assert.IsNull(piece.hex);
            Assert.IsNull(hex.piece);
            Assert.AreEqual(0, hexmapData.pieces.Count);
        }


        // Test handles adding new piece
        [Test]
        public void HandlesAddNewPiece()
        {
            Unit unit = Unit.CreateUnit(CardPaths.testUnit);
            Hex hex = hexmapData.GetHexAtHexCoords(Vector3Int.zero);
            hex.AddNewPiece(unit);

            Assert.AreEqual(1, hexmapData.pieces.Count);
            Assert.Contains(unit, hexmapData.pieces);
            Assert.AreEqual(1, unit.eventManager.onDeath.count);
        }


        // Test handles piece death
        [Test]
        public void HandlesPieceDeath()
        {
            Unit unit = Unit.CreateUnit(CardPaths.testUnit);
            Hex hex = hexmapData.GetHexAtHexCoords(Vector3Int.zero);
            hex.AddNewPiece(unit);
            unit.SetHex(hex);
            unit.eventManager.onDeath.OnEvent(unit);

            Assert.AreEqual(0, hexmapData.pieces.Count);
            Assert.IsFalse(hexmapData.pieces.Contains(unit));
            Assert.AreEqual(0, unit.eventManager.onDeath.count);
        }
    }
}