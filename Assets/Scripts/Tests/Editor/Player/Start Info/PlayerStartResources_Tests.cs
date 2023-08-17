using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayerTests
{
    public class PlayerStartResources_Tests
    {
        private PlayerStartResources startResources;


        // Setup
        [SetUp]
        public void Setup()
        {
            startResources = PlayerStartResources.LoadStartResources(StartResourcePaths.testStartResources);
        }


        // Test creates player start resources
        [Test]
        public void CreatesPlayerStartResources()
        {
            Assert.IsNotNull(startResources);
        }


        // Test sets info
        [Test]
        public void SetsStartResourcesInfo()
        {
            Assert.AreEqual(1, startResources.resources[ResourceType.Bone]);
            Assert.AreEqual(1, startResources.resources[ResourceType.Corpse]);
            Assert.AreEqual(1, startResources.resources[ResourceType.Mana]);
            Assert.AreEqual(1, startResources.resources[ResourceType.Stone]);
            Assert.AreEqual(1, startResources.resources[ResourceType.Wood]);
        }
    }
}