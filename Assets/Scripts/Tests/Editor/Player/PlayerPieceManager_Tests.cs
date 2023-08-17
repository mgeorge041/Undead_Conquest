using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayerTests
{
    public class PlayerPieceManager_Tests
    {
        private PlayerPieceManager pieceManager;


        // Setup
        [SetUp]
        public void Setup()
        {
            pieceManager = new PlayerPieceManager();
        }


        // Test creates player piece manager
        [Test]
        public void CreatesPlayerPieceManager()
        {
            Assert.IsNotNull(pieceManager);
        }


        // Test adds piece
        [Test]
        public void AddsNullPiece_ThrowsError()
        {
            Assert.Throws<System.ArgumentNullException>(() => pieceManager.AddPiece(null));
        }

        [Test]
        public void AddsPiece()
        {
            Unit unit = Unit.CreateUnit(CardPaths.testUnit);
            pieceManager.AddPiece(unit);

            Assert.IsTrue(pieceManager.HasPiece(unit));
        }


        // Test removes piece
        [Test]
        public void RemovesNullPiece_ThrowsError()
        {
            Assert.Throws<System.ArgumentNullException>(() => pieceManager.RemovePiece(null));
        }

        [Test]
        public void RemovesPiece()
        {
            Unit unit = Unit.CreateUnit(CardPaths.testUnit);
            pieceManager.AddPiece(unit);
            pieceManager.RemovePiece(unit);

            Assert.IsFalse(pieceManager.HasPiece(unit));
        }


        // Test sets selected piece
        [Test]
        public void SetsSelectedPiece()
        {
            int numEvents = 0;
            Piece selectedPiece = null;

            void HandleSetSelectedPiece(Piece piece)
            {
                numEvents++;
                selectedPiece = piece;
            }
            
            Unit unit = Unit.CreateUnit(CardPaths.testUnit);
            pieceManager.eventManager.onSetSelectedPiece.Subscribe(HandleSetSelectedPiece);
            pieceManager.SetSelectedPiece(unit);

            Assert.AreEqual(1, numEvents);
            Assert.AreEqual(unit, selectedPiece);
        }


        // Test subscribes to piece events
        [Test]
        public void SubscribesPieceDeathEvent_Unit()
        {
            Unit unit = Unit.CreateUnit(CardPaths.testUnit);
            pieceManager.AddPiece(unit);
            Assert.AreEqual(1, unit.eventManager.onDeath.count);
        }

        [Test]
        public void SubscribesPieceDeathEvent_Building()
        {
            HexmapData hexmapData = new HexmapData();
            Hex hex = hexmapData.GetHexAtHexCoords(Vector3Int.zero);
            Building building = Building.CreateBuilding(CardPaths.testBuilding);
            building.SetHex(hex);
            pieceManager.AddPiece(building);
            Assert.AreEqual(1, building.eventManager.onDeath.count);
        }

        [Test]
        public void SubscribesUnitFinishMoveEvent()
        {
            Unit unit = Unit.CreateUnit(CardPaths.testUnit);
            pieceManager.AddPiece(unit);
            Assert.AreEqual(1, unit.unitEventManager.onFinishMove.count);
        }


        // Test handling piece death
        [Test]
        public void HandlesPieceDeath_Unit() 
        {
            Unit unit = Unit.CreateUnit(CardPaths.testUnit);
            pieceManager.AddPiece(unit);
            unit.eventManager.onDeath.OnEvent(unit);

            Assert.IsFalse(pieceManager.HasPiece(unit));
            Assert.AreEqual(0, unit.eventManager.onDeath.count);
            Assert.AreEqual(0, unit.unitEventManager.onFinishMove.count);
        }

        [Test]
        public void HandlesPieceDeath_Building()
        {
            HexmapData hexmapData = new HexmapData();
            Hex hex = hexmapData.GetHexAtHexCoords(Vector3Int.zero);
            Building building = Building.CreateBuilding(CardPaths.testBuilding);
            building.SetHex(hex);
            pieceManager.AddPiece(building);
            building.eventManager.onDeath.OnEvent(building);

            Assert.IsFalse(pieceManager.HasPiece(building));
            Assert.AreEqual(0, building.eventManager.onDeath.count);
        }


        // Test domain hexes
        [Test]
        public void SetsDomainHexes_NullBuildingHex_ThrowsError()
        {
            Building building = Building.CreateBuilding(CardPaths.testBuilding);
            Assert.Throws<System.ArgumentNullException>(() => pieceManager.AddPiece(building));
        }

        [Test]
        public void SetsDomainHexes_1Building()
        {
            Building building = Building.CreateBuilding(CardPaths.testBuilding);
            HexmapData hexmapData = new HexmapData();
            hexmapData.AddPiece(building, Vector3Int.zero);
            pieceManager.AddPiece(building);

            Assert.AreEqual(19, pieceManager.domainHexes.Count);
        }

        [Test]
        public void SetsDomainHexes_2Buildings()
        {
            Building building = Building.CreateBuilding(CardPaths.testBuilding);
            Building building2 = Building.CreateBuilding(CardPaths.testBuilding);
            HexmapData hexmapData = new HexmapData();
            hexmapData.AddPiece(building, Vector3Int.zero);
            hexmapData.AddPiece(building2, new Vector3Int(2, -2, 0));
            pieceManager.AddPiece(building);
            pieceManager.AddPiece(building2);

            Assert.AreEqual(29, pieceManager.domainHexes.Count);
        }

        [Test]
        public void SetsDomainHexes_Death1Building()
        {
            Building building = Building.CreateBuilding(CardPaths.testBuilding);
            Building building2 = Building.CreateBuilding(CardPaths.testBuilding);
            HexmapData hexmapData = new HexmapData();
            hexmapData.AddPiece(building, Vector3Int.zero);
            hexmapData.AddPiece(building2, new Vector3Int(2, -2, 0));
            pieceManager.AddPiece(building);
            pieceManager.AddPiece(building2);
            building2.eventManager.onDeath.OnEvent(building2);

            Assert.AreEqual(19, pieceManager.domainHexes.Count);
        }


        // Test end turn
        [Test]
        public void EndsTurn()
        {
            Unit unit = Unit.CreateUnit(CardPaths.testUnit);
            Building building = Building.CreateBuilding(CardPaths.testBuilding);
            building.SetHex(new Hex(Vector3Int.zero));
            pieceManager.AddPiece(unit);
            pieceManager.AddPiece(building);

            unit.unitData.SetStat(PieceStatType.CurrentSpeed, 0);
            unit.unitData.SetHasActions(false);
            building.buildingData.SetHasActions(false);
            pieceManager.EndTurn();

            Assert.AreEqual(3, unit.unitData.GetStat(PieceStatType.CurrentSpeed));
            Assert.IsTrue(unit.unitData.hasActions);
            Assert.IsTrue(building.buildingData.hasActions);
        }


        // Test checking for unit domain buffs
        [Test]
        public void FiresEvent_HexAddPiece()
        {
            HexmapData hexmapData = new HexmapData();
            Building building = Building.CreateBuilding(CardPaths.testBuilding);
            Hex hex = hexmapData.GetHexAtHexCoords(Vector3Int.zero);
            hex.AddNewPiece(building);
            building.SetHex(hex);
            pieceManager.AddPiece(building);

            Unit unit = Unit.CreateUnit(CardPaths.testUnit);
            Hex unitHex = hexmapData.GetHexAtHexCoords(new Vector3Int(1, -1, 0));
            unitHex.AddNewPiece(unit);
            unit.SetHex(unitHex);
            pieceManager.AddPiece(unit);

            Assert.AreEqual(3, unit.unitData.attack);
            Assert.AreEqual(2, unit.unitData.defense);
        }

        [Test]
        public void FiresEvent_HexSetPiece()
        {
            HexmapData hexmapData = new HexmapData();
            Building building = Building.CreateBuilding(CardPaths.testBuilding);
            Hex hex = hexmapData.GetHexAtHexCoords(Vector3Int.zero);
            hex.AddNewPiece(building);
            building.SetHex(hex);
            pieceManager.AddPiece(building);

            Unit unit = Unit.CreateUnit(CardPaths.testUnit);
            Hex unitHex = hexmapData.GetHexAtHexCoords(new Vector3Int(1, -1, 0));
            unitHex.SetPiece(unit);
            unit.SetHex(unitHex);
            pieceManager.AddPiece(unit);

            Assert.AreEqual(3, unit.unitData.attack);
            Assert.AreEqual(2, unit.unitData.defense);
        }

        [Test]
        public void FiresEvent_UnitMoveOutDomain()
        {
            HexmapData hexmapData = new HexmapData();
            Building building = Building.CreateBuilding(CardPaths.testBuilding);
            Hex hex = hexmapData.GetHexAtHexCoords(Vector3Int.zero);
            hex.AddNewPiece(building);
            building.SetHex(hex);
            pieceManager.AddPiece(building);

            Unit unit = Unit.CreateUnit(CardPaths.testUnit);
            Hex unitHex = hexmapData.GetHexAtHexCoords(new Vector3Int(1, -1, 0));
            unitHex.AddNewPiece(unit);
            unit.SetHex(unitHex);
            pieceManager.AddPiece(unit);

            Hex moveHex = hexmapData.GetHexAtHexCoords(new Vector3Int(3, -3, 0));
            unit.MoveToHex(moveHex);

            Assert.AreEqual(2, unit.unitData.attack);
            Assert.AreEqual(1, unit.unitData.defense);
        }

        [Test]
        public void FiresEvent_UnitMoveIntoDomain()
        {
            HexmapData hexmapData = new HexmapData();
            Building building = Building.CreateBuilding(CardPaths.testBuilding);
            Hex hex = hexmapData.GetHexAtHexCoords(Vector3Int.zero);
            hex.AddNewPiece(building);
            building.SetHex(hex);
            pieceManager.AddPiece(building);

            Unit unit = Unit.CreateUnit(CardPaths.testUnit);
            Hex unitHex = hexmapData.GetHexAtHexCoords(new Vector3Int(3, -3, 0));
            unitHex.AddNewPiece(unit);
            unit.SetHex(unitHex);
            pieceManager.AddPiece(unit);

            Hex moveHex = hexmapData.GetHexAtHexCoords(new Vector3Int(1, -1, 0));
            unit.MoveToHex(moveHex);

            Assert.AreEqual(3, unit.unitData.attack);
            Assert.AreEqual(2, unit.unitData.defense);
        }
    }
}