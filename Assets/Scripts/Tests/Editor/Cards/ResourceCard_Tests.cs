using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CardTests
{
    public class ResourceCard_Tests
    {
        private ResourceCard card;


        // Setup
        [SetUp]
        public void Setup()
        {
            card = ResourceCard.CreateResourceCard();
        }


        // Test creates resource card
        [Test]
        public void CreatesResourceCard()
        {
            Assert.IsNotNull(card);
        }


        // Test sets info
        [Test]
        public void SetsInfo_1Resource()
        {
            card = ResourceCard.CreateResourceCard(CardPaths.testBoneResource);

            Assert.AreEqual(1, int.Parse(card.resourceAmount1Label.text));
            Assert.AreEqual(ResourceCard.LoadResourceSprite(ResourceType.Bone), card.resourceIcon1.sprite);
            Assert.IsTrue(card.resourceContainer1.gameObject.activeSelf);
            Assert.IsFalse(card.resourceContainer2.gameObject.activeSelf);
            Assert.IsFalse(card.resourceContainer3.gameObject.activeSelf);
        }

        [Test]
        public void SetsInfo_2Resources()
        {
            card = ResourceCard.CreateResourceCard(CardPaths.testBoneStoneResource);

            Assert.AreEqual(1, int.Parse(card.resourceAmount1Label.text));
            Assert.AreEqual(ResourceCard.LoadResourceSprite(ResourceType.Bone), card.resourceIcon1.sprite);
            Assert.AreEqual(1, int.Parse(card.resourceAmount2Label.text));
            Assert.AreEqual(ResourceCard.LoadResourceSprite(ResourceType.Stone), card.resourceIcon2.sprite);
            Assert.IsTrue(card.resourceContainer1.gameObject.activeSelf);
            Assert.IsTrue(card.resourceContainer2.gameObject.activeSelf);
            Assert.IsFalse(card.resourceContainer3.gameObject.activeSelf);
        }

        [Test]
        public void SetsInfo_3Resources()
        {
            card = ResourceCard.CreateResourceCard(CardPaths.testBoneStoneWoodResource);

            Assert.AreEqual(1, int.Parse(card.resourceAmount1Label.text));
            Assert.AreEqual(ResourceCard.LoadResourceSprite(ResourceType.Bone), card.resourceIcon1.sprite);
            Assert.AreEqual(1, int.Parse(card.resourceAmount2Label.text));
            Assert.AreEqual(ResourceCard.LoadResourceSprite(ResourceType.Stone), card.resourceIcon2.sprite);
            Assert.AreEqual(1, int.Parse(card.resourceAmount3Label.text));
            Assert.AreEqual(ResourceCard.LoadResourceSprite(ResourceType.Wood), card.resourceIcon3.sprite);
            Assert.IsTrue(card.resourceContainer1.gameObject.activeSelf);
            Assert.IsTrue(card.resourceContainer2.gameObject.activeSelf);
            Assert.IsTrue(card.resourceContainer3.gameObject.activeSelf);
        }
    }
}