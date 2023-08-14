using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;

namespace PlayerTests.PlayerUITests
{
    public class HandPanel_Tests
    {
        private HandPanel panel;


        // Setup
        [SetUp]
        public void Setup()
        {
            panel = HandPanel.CreateHandPanel();
        }


        // Test creates hand panel
        [Test]
        public void CreatesHandPanel()
        {
            Assert.IsNotNull(panel);
        }


        // Test initializes
        [Test]
        public void Initializes()
        {
            Assert.IsTrue(panel.initialized);
        }


        // Test resets
        [Test]
        public void Resets()
        {
            ResourceCard card = ResourceCard.CreateResourceCard();
            panel.AddCard(card);
            panel.Reset();
            Assert.AreEqual(0, panel.numCards);
        }


        // Test adding cards
        [Test]
        public void AddsCard()
        {
            ResourceCard card = ResourceCard.CreateResourceCard();
            panel.AddCard(card);
            Assert.AreEqual(1, panel.numCards);
            Assert.AreEqual(1, int.Parse(panel.numHandCardsLabel.text));
        }


        // Test removing cards
        [Test]
        public void RemovesCard()
        {
            ResourceCard card = ResourceCard.CreateResourceCard();
            panel.AddCard(card);
            panel.RemoveCard(card);
            Assert.AreEqual(0, panel.numCards);
            Assert.IsFalse(card.gameObject.activeSelf);
            Assert.AreEqual(0, int.Parse(panel.numHandCardsLabel.text));
        }


        // Test setting num cards label
        [Test]
        public void SetsNumberDeckCardsLabel()
        {
            panel.SetNumDeckCards(0, 1);
            string label = "0/1";
            Assert.AreEqual(label, panel.numDeckCardsLabel.text);
        }

        [Test]
        public void SetsNumberHandCardsLabel()
        {
            panel.SetNumHandCards(1);
            string label = "1";
            Assert.AreEqual(label, panel.numHandCardsLabel.text);
        }

        [Test]
        public void SetsNumberDiscardCardsLabel()
        {
            panel.SetNumDiscardCards(1);
            string label = "1";
            Assert.AreEqual(label, panel.numDiscardCardsLabel.text);
        }

        [Test]
        public void SetsNumberTotalCardsLabel()
        {
            panel.SetNumDeckCards(0, 1);
            string label = "0/2";
            panel.SetNumTotalDeckCards(2);
            Assert.AreEqual(label, panel.numDeckCardsLabel.text);
        }
    }
}