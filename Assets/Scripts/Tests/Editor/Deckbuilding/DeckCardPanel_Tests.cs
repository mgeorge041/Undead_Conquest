using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace DeckbuildingTests
{
    public class DeckCardPanel_Tests
    {
        private DeckCardPanel panel;

        private int numClickAddButtonEvents;
        private int numClickRemoveButtonEvents;
        private CardInfo cardInfo;


        // Setup
        [SetUp]
        public void Setup()
        {
            panel = DeckCardPanel.CreateDeckCardPanel();

            numClickAddButtonEvents = 0;
            numClickRemoveButtonEvents = 0;
            cardInfo = null;
        }


        // Test creates deck card panel
        [Test]
        public void CreatesDeckCardPanel()
        {
            Assert.IsNotNull(panel);
        }


        // Test sets card info
        [Test]
        public void SetsCardInfo()
        {
            CardInfo cardInfo = CardInfo.LoadCardInfo(CardPaths.testUnit);
            panel.SetCardPanelInfo(cardInfo);

            Assert.AreEqual("Test Unit", panel.cardNameLabel.text);
        }


        // Test gets card count
        [Test]
        public void GetsCardCount()
        {
            Assert.AreEqual(0, panel.GetCardCount());
        }


        // Test increments card count
        [Test]
        public void IncrementsCardCount_Negative()
        {
            panel.IncrementCardCount(-1);
            Assert.AreEqual(0, panel.GetCardCount());
        }

        [Test]
        public void IncrementsCardCount_0()
        {
            panel.IncrementCardCount(0);
            Assert.AreEqual(0, panel.GetCardCount());
        }

        [Test]
        public void IncrementsCardCount_1()
        {
            panel.IncrementCardCount(1);
            Assert.AreEqual(1, panel.GetCardCount());
        }


        // Test fires events
        private void HandleClickAddCardButton(CardInfo cardInfo)
        {
            numClickAddButtonEvents++;
            this.cardInfo = cardInfo;
        }
        private void HandleClickRemoveCardButton(CardInfo cardInfo)
        {
            numClickRemoveButtonEvents++;
            this.cardInfo = cardInfo;
        }

        [Test]
        public void FiresEvent_ClickAddCardButton()
        {
            panel.eventManager.onAddCard.Subscribe(HandleClickAddCardButton);
            CardInfo cardInfo = CardInfo.LoadCardInfo(CardPaths.testUnit);
            panel.SetCardPanelInfo(cardInfo);
            panel.ClickAddCardButton();

            Assert.AreEqual(1, numClickAddButtonEvents);
            Assert.AreEqual("Test Unit", cardInfo.cardName);
        }

        [Test]
        public void FiresEvent_ClickRemoveCardButton()
        {
            panel.eventManager.onRemoveCard.Subscribe(HandleClickRemoveCardButton);
            CardInfo cardInfo = CardInfo.LoadCardInfo(CardPaths.testUnit);
            panel.SetCardPanelInfo(cardInfo);
            panel.ClickRemoveCardButton();

            Assert.AreEqual(1, numClickRemoveButtonEvents);
            Assert.AreEqual("Test Unit", cardInfo.cardName);
        }
    }
}