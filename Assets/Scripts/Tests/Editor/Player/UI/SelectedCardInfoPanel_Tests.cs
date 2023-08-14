using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayerTests.PlayerUITests
{
    public class SelectedCardInfoPanel_Tests
    {
        private SelectedCardInfoPanel panel;
        private int numShowHandEvents;


        // Setup
        [SetUp]
        public void Setup()
        {
            panel = SelectedCardInfoPanel.CreateSelectedCardInfoPanel();
            numShowHandEvents = 0;
        }


        // Test creates selected card info panel
        [Test]
        public void CreatesSelectedCardInfoPanel()
        {
            Assert.IsNotNull(panel);
        }


        // Test sets card info
        [Test]
        public void SetsCardInfo_NullCard()
        {
            panel.SetSelectedCardInfo(null);
            Assert.IsFalse(panel.gameObject.activeSelf);
        }

        [Test]
        public void SetsCardInfo_ValidCard()
        {
            UnitCard unitCard = UnitCard.CreateUnitCard(CardPaths.testUnit);
            panel.SetSelectedCardInfo(unitCard);

            Assert.AreEqual("Test Unit", panel.cardNameLabel.text);
            Assert.AreEqual(ResourceCard.LoadResourceSprite(ResourceType.Bone), panel.resourceContainer1.resourceIcon.sprite);
            Assert.AreEqual(ResourceCard.LoadResourceSprite(ResourceType.Stone), panel.resourceContainer2.resourceIcon.sprite);
            Assert.AreEqual(2, int.Parse(panel.resourceContainer1.resourceAmount.text));
            Assert.AreEqual(1, int.Parse(panel.resourceContainer2.resourceAmount.text));
            Assert.IsFalse(panel.resourceContainer3.gameObject.activeSelf);
        }


        // Test showing and hiding hand
        private void HandleShowHandEvent(bool showHand)
        {
            numShowHandEvents++;
        }

        [Test]
        public void ShowsHand()
        {
            panel.eventManager.onClickShowHandButton.Subscribe(HandleShowHandEvent);
            panel.ShowHand(true);
            Assert.IsFalse(panel.showHandButton.gameObject.activeSelf);
            Assert.IsTrue(panel.hideHandButton.gameObject.activeSelf);
            Assert.AreEqual(1, numShowHandEvents);
        }

        [Test]
        public void HidesHand()
        {
            panel.eventManager.onClickShowHandButton.Subscribe(HandleShowHandEvent);
            panel.ShowHand(false);
            Assert.IsTrue(panel.showHandButton.gameObject.activeSelf);
            Assert.IsFalse(panel.hideHandButton.gameObject.activeSelf);
            Assert.AreEqual(1, numShowHandEvents);
        }
    }
}