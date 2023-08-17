using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayerTests.PlayerUITests
{
    public class PlayerUI_Tests
    {
        private PlayerUI ui;


        // Setup
        [SetUp]
        public void Setup()
        {
            ui = PlayerUI.CreatePlayerUI();
        }


        // Test creates player UI
        [Test]
        public void CreatesPlayerUI()
        {
            Assert.IsNotNull(ui);
        }


        // Test initializes components
        [Test]
        public void InitializesComponents()
        {
            Assert.IsTrue(ui.resourcePanel.initialized);
            Assert.IsTrue(ui.handPanel.initialized);
            Assert.IsTrue(ui.initialized);
        }


        // Test sets initial resources
        [Test]
        public void SetsInitialResources()
        {
            PlayerItemManager itemManager = new PlayerItemManager();
            PlayerStartResources startResources = PlayerStartResources.LoadStartResources(StartResourcePaths.testStartResources);
            itemManager.resourceManager.AddResources(startResources.resources);
            ui.SetItemManagerInfo(itemManager);

            Assert.AreEqual(1, ui.resourcePanel.GetResource(ResourceType.Bone));
            Assert.AreEqual(1, ui.resourcePanel.GetResource(ResourceType.Corpse));
            Assert.AreEqual(1, ui.resourcePanel.GetResource(ResourceType.Mana));
            Assert.AreEqual(1, ui.resourcePanel.GetResource(ResourceType.Stone));
            Assert.AreEqual(1, ui.resourcePanel.GetResource(ResourceType.Wood));
        }
    }
}