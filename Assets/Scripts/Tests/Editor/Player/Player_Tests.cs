using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayerTests
{
    public class Player_Tests
    {
        private Player player;


        // Setup
        [SetUp]
        public void Setup()
        {
            player = Player.CreatePlayer();
        }


        // Test creates player
        [Test]
        public void CreatesPlayer()
        {
            Assert.IsNotNull(player);
        }


        // Test initializes components
        [Test]
        public void InitializesComponents()
        {
            Assert.IsTrue(player.inputController.initialized);
            Assert.IsTrue(player.playerUI.initialized);
            Assert.IsTrue(player.initialized);
        }


        // Test sets initial resources
        [Test]
        public void SetsStartResources()
        {
            PlayerStartResources startResources = PlayerStartResources.LoadStartResources(StartResourcePaths.testStartResources);
            player.startResources = startResources;
            player.Initialize();

            Assert.AreEqual(1, player.itemManager.resourceManager.GetResource(ResourceType.Bone));
            Assert.AreEqual(1, player.itemManager.resourceManager.GetResource(ResourceType.Corpse));
            Assert.AreEqual(1, player.itemManager.resourceManager.GetResource(ResourceType.Mana));
            Assert.AreEqual(1, player.itemManager.resourceManager.GetResource(ResourceType.Stone));
            Assert.AreEqual(1, player.itemManager.resourceManager.GetResource(ResourceType.Wood));
        }


        // Test sets start pieces when setting hexmap data
        [Test]
        public void SetsHexmapData_HasStartPieces()
        {
            PlayerStartPieces startPieces = PlayerStartPieces.LoadStartPieces(StartPiecesPaths.testStartPieces);
            player.startPieces = startPieces;
            HexmapData hexmapData = new HexmapData();
            player.SetHexmapData(hexmapData);
            Vector3Int startCoords = new Vector3Int(1, -1, 0);
            Hex hex = hexmapData.GetHexAtHexCoords(startCoords);

            Assert.AreEqual(1, player.itemManager.pieceManager.pieces.Count);
            Assert.AreEqual(1, hexmapData.pieces.Count);
            Assert.IsTrue(hex.hasPiece);
            Assert.AreEqual("Test Unit", hex.piece.playableCardInfo.cardName);
        }
    }
}