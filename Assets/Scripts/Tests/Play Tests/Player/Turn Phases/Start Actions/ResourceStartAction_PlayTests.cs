using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PieceTests.StartActionTests
{
    public class ResourceStartAction_Tests
    {
        private ResourceStartAction startAction;
        private PlayerItemManager itemManager;


        // Setup
        [SetUp]
        public void Setup()
        {
            startAction = StartAction.CreateStartAction<ResourceStartAction>(StartActionPaths.testResourceAction);
            itemManager = new PlayerItemManager();
        }


        // Test creates resource start action
        [Test]
        public void CreatesResourceStartAction()
        {
            Assert.IsNotNull(startAction);
        }


        // Test adding resources
        [UnityTest]
        public IEnumerator AddsResources()
        {
            Piece piece = Piece.CreatePiece(CardPaths.testUnit);
            startAction.PerformStartAction(piece, itemManager);
            yield return new WaitForSeconds(2);
            Assert.AreEqual(1, itemManager.resources.GetResource(ResourceType.Bone));
            Assert.AreEqual(1, itemManager.resources.GetResource(ResourceType.Stone));
        }
    }
}
