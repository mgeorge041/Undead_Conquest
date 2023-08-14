using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayerTests.PlayerUITests
{
    public class ResourcePanel_Tests
    {
        private ResourcePanel panel;


        // Setup
        [SetUp]
        public void Setup()
        {
            panel = ResourcePanel.CreateResourcePanel();
        }


        // Test creates resource panel
        [Test]
        public void CreatesResourcePanel()
        {
            Assert.IsNotNull(panel);
        }


        // Test getting resources
        [Test]
        public void Initializes()
        {
            Assert.AreEqual(0, panel.GetResource(ResourceType.Bone));
            Assert.AreEqual(0, panel.GetResource(ResourceType.Corpse));
            Assert.AreEqual(0, panel.GetResource(ResourceType.Mana));
            Assert.AreEqual(0, panel.GetResource(ResourceType.Stone));
            Assert.AreEqual(0, panel.GetResource(ResourceType.Wood));
            Assert.IsTrue(panel.initialized);
        }


        // Test resets
        [Test]
        public void Resets()
        {
            int amount = 1;
            panel.SetResource(ResourceType.Bone, amount);
            panel.Reset();
            Assert.AreEqual(0, panel.GetResource(ResourceType.Bone));
        }


        // Test setting individual resource
        [Test]
        public void SetsResourceAmount_Bone()
        {
            int amount = 1;
            panel.SetResource(ResourceType.Bone, amount);
            Assert.AreEqual(1, panel.GetResource(ResourceType.Bone));
        }

        [Test]
        public void SetsResourceAmount_Corpse()
        {
            int amount = 1;
            panel.SetResource(ResourceType.Corpse, amount);
            Assert.AreEqual(1, panel.GetResource(ResourceType.Corpse));
        }

        [Test]
        public void SetsResourceAmount_Mana()
        {
            int amount = 1;
            panel.SetResource(ResourceType.Mana, amount);
            Assert.AreEqual(1, panel.GetResource(ResourceType.Mana));
        }

        [Test]
        public void SetsResourceAmount_Stone()
        {
            int amount = 1;
            panel.SetResource(ResourceType.Stone, amount);
            Assert.AreEqual(1, panel.GetResource(ResourceType.Stone));
        }

        [Test]
        public void SetsResourceAmount_Wood()
        {
            int amount = 1;
            panel.SetResource(ResourceType.Wood, amount);
            Assert.AreEqual(1, panel.GetResource(ResourceType.Wood));
        }
    }
}