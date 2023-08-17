using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace DeckbuildingTests
{
    public class DeckListManager_Tests
    {
        private DeckListManager listManager;


        // Setup
        [SetUp]
        public void Setup()
        {
            listManager = new DeckListManager();
        }


        // Test creates deck list manager
        [Test]
        public void CreatesDeckListManager()
        {
            Assert.IsNotNull(listManager);
        }


        // Test adds card info
        [Test]
        public void AddsCardInfo_Null_ThrowsError()
        {
            Assert.Throws<System.ArgumentNullException>(() => listManager.HandleAddCardInfo(null));
        }

        [Test]
        public void AddsCardInfo_EmptyList()
        {
            CardInfo cardInfo = CardInfo.LoadCardInfo(CardPaths.testUnit);
            listManager.HandleAddCardInfo(cardInfo);

            Assert.AreEqual(1, listManager.numTotalCards);
            Assert.AreEqual(1, listManager.deckList.Count);
            Assert.AreEqual(1, listManager.cardPanels.Count);
            Assert.AreEqual("Test Unit", listManager.cardPanels[cardInfo].cardNameLabel.text);
            Assert.AreEqual(1, listManager.cardPanels[cardInfo].GetCardCount());
        }

        [Test]
        public void AddsCardInfo_SameCard()
        {
            CardInfo cardInfo = CardInfo.LoadCardInfo(CardPaths.testUnit);
            listManager.HandleAddCardInfo(cardInfo);
            listManager.HandleAddCardInfo(cardInfo);

            Assert.AreEqual(2, listManager.numTotalCards);
            Assert.AreEqual(1, listManager.deckList.Count);
            Assert.AreEqual(1, listManager.cardPanels.Count);
            Assert.AreEqual("Test Unit", listManager.cardPanels[cardInfo].cardNameLabel.text);
            Assert.AreEqual(2, listManager.cardPanels[cardInfo].GetCardCount());
        }

        [Test]
        public void AddsCardInfo_NewCard()
        {
            CardInfo cardInfo = CardInfo.LoadCardInfo(CardPaths.testUnit);
            CardInfo cardInfo2 = CardInfo.LoadCardInfo(CardPaths.testBuilding);
            listManager.HandleAddCardInfo(cardInfo);
            listManager.HandleAddCardInfo(cardInfo2);

            Assert.AreEqual(2, listManager.numTotalCards);
            Assert.AreEqual(2, listManager.deckList.Count);
            Assert.AreEqual(2, listManager.cardPanels.Count);
            Assert.AreEqual("Test Unit", listManager.cardPanels[cardInfo].cardNameLabel.text);
            Assert.AreEqual(1, listManager.cardPanels[cardInfo].GetCardCount());
            Assert.AreEqual("Test Building", listManager.cardPanels[cardInfo2].cardNameLabel.text);
            Assert.AreEqual(1, listManager.cardPanels[cardInfo2].GetCardCount());
        }

        [Test]
        public void AddsCardResourceInfo_Building()
        {
            CardInfo cardInfo = CardInfo.LoadCardInfo(CardPaths.testBuilding);
            listManager.HandleAddCardInfo(cardInfo);

            Assert.AreEqual(1, listManager.cardResourceCosts[ResourceType.Stone]);
            Assert.AreEqual(1, listManager.cardResourceCosts[ResourceType.Wood]);
        }

        [Test]
        public void AddsCardResourceInfo_Unit()
        {
            CardInfo cardInfo = CardInfo.LoadCardInfo(CardPaths.testUnit);
            listManager.HandleAddCardInfo(cardInfo);

            Assert.AreEqual(2, listManager.cardResourceCosts[ResourceType.Bone]);
            Assert.AreEqual(1, listManager.cardResourceCosts[ResourceType.Stone]);
        }

        [Test]
        public void AddsCardResourceInfo_Resource()
        {
            CardInfo cardInfo = CardInfo.LoadCardInfo(CardPaths.testBoneResource);
            listManager.HandleAddCardInfo(cardInfo);

            Assert.AreEqual(1, listManager.cardResourceProvides[ResourceType.Bone]);
        }


        // Test removes card info
        [Test]
        public void RemovesCardInfo_Null_ThrowsError()
        {
            Assert.Throws<System.ArgumentNullException>(() => listManager.HandleRemoveCardInfo(null));
        }

        [Test]
        public void RemovesCardInfo_EmptyList_ThrowsError()
        {
            CardInfo cardInfo = CardInfo.LoadCardInfo(CardPaths.testUnit);
            Assert.Throws<System.ArgumentException>(() => listManager.HandleRemoveCardInfo(cardInfo));
        }

        [Test]
        public void RemovesCardInfo_1SameCard()
        {
            CardInfo cardInfo = CardInfo.LoadCardInfo(CardPaths.testUnit);
            listManager.HandleAddCardInfo(cardInfo);
            listManager.HandleRemoveCardInfo(cardInfo);

            Assert.AreEqual(0, listManager.numTotalCards);
            Assert.AreEqual(1, listManager.deckList.Count);
            Assert.AreEqual(1, listManager.cardPanels.Count);
            Assert.AreEqual(0, listManager.cardPanels[cardInfo].GetCardCount());
            Assert.IsFalse(listManager.cardPanels[cardInfo].gameObject.activeSelf);
        }

        [Test]
        public void RemovesCardInfo_2SameCard()
        {
            CardInfo cardInfo = CardInfo.LoadCardInfo(CardPaths.testUnit);
            listManager.HandleAddCardInfo(cardInfo);
            listManager.HandleAddCardInfo(cardInfo);
            listManager.HandleRemoveCardInfo(cardInfo);

            Assert.AreEqual(1, listManager.numTotalCards);
            Assert.AreEqual(1, listManager.deckList.Count);
            Assert.AreEqual(1, listManager.cardPanels.Count);
            Assert.AreEqual(1, listManager.cardPanels[cardInfo].GetCardCount());
        }

        [Test]
        public void RemovesCardResourceInfo_Building()
        {
            CardInfo cardInfo = CardInfo.LoadCardInfo(CardPaths.testBuilding);
            listManager.HandleAddCardInfo(cardInfo);
            listManager.HandleRemoveCardInfo(cardInfo);

            Assert.AreEqual(0, listManager.cardResourceCosts[ResourceType.Stone]);
            Assert.AreEqual(0, listManager.cardResourceCosts[ResourceType.Wood]);
        }

        [Test]
        public void RemovesCardResourceInfo_Unit()
        {
            CardInfo cardInfo = CardInfo.LoadCardInfo(CardPaths.testUnit);
            listManager.HandleAddCardInfo(cardInfo);
            listManager.HandleRemoveCardInfo(cardInfo);

            Assert.AreEqual(0, listManager.cardResourceCosts[ResourceType.Bone]);
            Assert.AreEqual(0, listManager.cardResourceCosts[ResourceType.Stone]);
        }

        [Test]
        public void RemovesCardResourceInfo_Resource()
        {
            CardInfo cardInfo = CardInfo.LoadCardInfo(CardPaths.testBoneResource);
            listManager.HandleAddCardInfo(cardInfo);
            listManager.HandleRemoveCardInfo(cardInfo);

            Assert.AreEqual(0, listManager.cardResourceProvides[ResourceType.Bone]);
        }
    }
}