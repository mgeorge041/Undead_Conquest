using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayerTests
{
    public class PlayerResources_Tests
    {
        private PlayerResources resources;


        // Setup
        [SetUp]
        public void Setup()
        {
            resources = new PlayerResources();
        }


        // Test creates player resources
        [Test]
        public void CreatesPlayerResources()
        {
            Assert.IsNotNull(resources);
        }


        // Test reset
        [Test]
        public void Resets()
        {
            int amount = 1;
            resources.SetResource(ResourceType.Bone, amount);
            resources.Reset();
            Assert.AreEqual(0, resources.GetResource(ResourceType.Bone));
        }


        // Test getting resources
        [Test]
        public void GetsResources_EmptyResourceDict()
        {
            Assert.AreEqual(0, resources.GetResource(ResourceType.Bone));
            Assert.AreEqual(0, resources.GetResource(ResourceType.Corpse));
            Assert.AreEqual(0, resources.GetResource(ResourceType.Mana));
            Assert.AreEqual(0, resources.GetResource(ResourceType.Stone));
            Assert.AreEqual(0, resources.GetResource(ResourceType.Wood));
        }


        // Test setting resources
        [Test]
        public void SetsResource_Bone()
        {
            int amount = 1;
            resources.SetResource(ResourceType.Bone, amount);
            Assert.AreEqual(amount, resources.GetResource(ResourceType.Bone));
        }

        [Test]
        public void SetsResource_Corpse()
        {
            int amount = 1;
            resources.SetResource(ResourceType.Corpse, amount);
            Assert.AreEqual(amount, resources.GetResource(ResourceType.Corpse));
        }

        [Test]
        public void SetsResource_Mana()
        {
            int amount = 1;
            resources.SetResource(ResourceType.Mana, amount);
            Assert.AreEqual(amount, resources.GetResource(ResourceType.Mana));
        }

        [Test]
        public void SetsResource_Stone()
        {
            int amount = 1;
            resources.SetResource(ResourceType.Stone, amount);
            Assert.AreEqual(amount, resources.GetResource(ResourceType.Stone));
        }

        [Test]
        public void SetsResource_Wood()
        {
            int amount = 1;
            resources.SetResource(ResourceType.Wood, amount);
            Assert.AreEqual(amount, resources.GetResource(ResourceType.Wood));
        }


        // Test adding resources
        [Test]
        public void AddsResource_Bone()
        {
            int amount = 1;
            resources.AddResource(ResourceType.Bone, amount);
            Assert.AreEqual(amount, resources.GetResource(ResourceType.Bone));
        }

        [Test]
        public void AddsResource_Corpse()
        {
            int amount = 1;
            resources.AddResource(ResourceType.Corpse, amount);
            Assert.AreEqual(amount, resources.GetResource(ResourceType.Corpse));
        }

        [Test]
        public void AddsResource_Mana()
        {
            int amount = 1;
            resources.AddResource(ResourceType.Mana, amount);
            Assert.AreEqual(amount, resources.GetResource(ResourceType.Mana));
        }

        [Test]
        public void AddsResource_Stone()
        {
            int amount = 1;
            resources.AddResource(ResourceType.Stone, amount);
            Assert.AreEqual(amount, resources.GetResource(ResourceType.Stone));
        }

        [Test]
        public void AddsResource_Wood()
        {
            int amount = 1;
            resources.AddResource(ResourceType.Wood, amount);
            Assert.AreEqual(amount, resources.GetResource(ResourceType.Wood));
        }

        [Test]
        public void AddsNegativeResource_StaysAt0()
        {
            int amount = -1;
            resources.AddResource(ResourceType.Bone, amount);
            Assert.AreEqual(0, resources.GetResource(ResourceType.Bone));
        }


        // Test removing resources
        [Test]
        public void RemovesResource_Bone()
        {
            int amount = 1;
            resources.SetResource(ResourceType.Bone, amount);
            resources.RemoveResource(ResourceType.Bone, amount);
            Assert.AreEqual(0, resources.GetResource(ResourceType.Bone));
        }

        [Test]
        public void RemovesResource_Corpse()
        {
            int amount = 1;
            resources.SetResource(ResourceType.Corpse, amount);
            resources.RemoveResource(ResourceType.Corpse, amount);
            Assert.AreEqual(0, resources.GetResource(ResourceType.Corpse));
        }

        [Test]
        public void RemovesResource_Mana()
        {
            int amount = 1;
            resources.SetResource(ResourceType.Mana, amount);
            resources.RemoveResource(ResourceType.Mana, amount);
            Assert.AreEqual(0, resources.GetResource(ResourceType.Mana));
        }

        [Test]
        public void RemovesResource_Stone()
        {
            int amount = 1;
            resources.SetResource(ResourceType.Stone, amount);
            resources.RemoveResource(ResourceType.Stone, amount);
            Assert.AreEqual(0, resources.GetResource(ResourceType.Stone));
        }

        [Test]
        public void RemovesResource_Wood()
        {
            int amount = 1;
            resources.SetResource(ResourceType.Wood, amount);
            resources.RemoveResource(ResourceType.Wood, amount);
            Assert.AreEqual(0, resources.GetResource(ResourceType.Wood));
        }

        [Test]
        public void RemovesResource_StaysAt0()
        {
            int amount = 1;
            resources.RemoveResource(ResourceType.Bone, amount);
            Assert.AreEqual(0, resources.GetResource(ResourceType.Bone));
        }


        // Test can play card
        [Test]
        public void CantPlayCard_NoResources()
        {
            Card card = Card.CreateCard<UnitCard>(CardPaths.testUnit);
            bool canPlay = resources.CanPlayCard(card);
            Assert.IsFalse(canPlay);
        }

        [Test]
        public void CantPlayCard_InsufficientResources_Resource1()
        {
            Card card = Card.CreateCard<UnitCard>(CardPaths.testUnit);
            resources.SetResource(ResourceType.Bone, 1);
            bool canPlay = resources.CanPlayCard(card);
            Assert.IsFalse(canPlay);
        }

        [Test]
        public void CantPlayCard_InsufficientResources_Resource2()
        {
            Card card = Card.CreateCard<UnitCard>(CardPaths.testUnit);
            resources.SetResource(ResourceType.Bone, 2);
            bool canPlay = resources.CanPlayCard(card);
            Assert.IsFalse(canPlay);
        }

        [Test]
        public void CanPlayCard_SufficientResources()
        {
            Card card = Card.CreateCard<UnitCard>(CardPaths.testUnit);
            resources.SetResource(ResourceType.Bone, 2);
            resources.SetResource(ResourceType.Stone, 1);
            bool canPlay = resources.CanPlayCard(card);
            Assert.IsTrue(canPlay);
        }


        // Test playing card
        [Test]
        public void PlaysCard()
        {
            Card card = Card.CreateCard<UnitCard>(CardPaths.testUnit);
            resources.SetResource(ResourceType.Bone, 2);
            resources.SetResource(ResourceType.Stone, 1);
            resources.PlayCard(card);
            Assert.AreEqual(0, resources.GetResource(ResourceType.Bone));
            Assert.AreEqual(0, resources.GetResource(ResourceType.Stone));
        }
    }
}