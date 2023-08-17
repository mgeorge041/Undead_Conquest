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


        // Test setting start action info
        [Test]
        public void SetsActionInfo()
        {
            Assert.AreEqual(StartActionType.Resources, startAction.actionType);
            Assert.AreEqual(ResourceType.Bone, startAction.resourceActionInfo.resourceType1);
            Assert.AreEqual(1, startAction.resourceActionInfo.resourceAmount1);
            Assert.AreEqual(ResourceType.Stone, startAction.resourceActionInfo.resourceType2);
            Assert.AreEqual(1, startAction.resourceActionInfo.resourceAmount2);
        }

        [Test]
        public void DoesntAddResources_TurnCooldown()
        {
            startAction.SetTurnCooldown(1);
            startAction.PerformStartAction(null, itemManager);
            Assert.AreEqual(0, itemManager.resourceManager.GetResource(ResourceType.Bone));
            Assert.AreEqual(0, itemManager.resourceManager.GetResource(ResourceType.Stone));
            Assert.AreEqual(0, startAction.turnCooldown);
        }
    }
}