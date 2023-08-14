using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayerTests.PlayerUITests
{
    public class ResourceCountContainer_Tests
    {
        private ResourceCountContainer container;


        // Setup
        [SetUp]
        public void Setup()
        {
            container = ResourceCountContainer.CreateResourceCountContainer();
        }


        // Test creates resource count container
        [Test]
        public void CreatesResourceCountContainer()
        {
            Assert.IsNotNull(container);
        }


        // Test sets info
        [Test]
        public void SetsResourceInfo_NoneType()
        {
            ResourceType type = ResourceType.None;
            int amount = 1;
            container.SetResourceInformation(type, amount);
            Assert.IsNull(container.resourceIcon.sprite);
            Assert.AreEqual(1, int.Parse(container.resourceAmount.text));
        }

        [Test]
        public void SetsResourceInfo_ValidType()
        {
            ResourceType type = ResourceType.Bone;
            int amount = 1;
            container.SetResourceInformation(type, amount);
            Assert.AreEqual(ResourceCard.LoadResourceSprite(type), container.resourceIcon.sprite);
            Assert.AreEqual(1, int.Parse(container.resourceAmount.text));
        }
    }
}