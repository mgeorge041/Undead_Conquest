using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace DeckbuildingTests
{
    public class DeckStatsPanel_Tests
    {
        private DeckStatsPanel panel;


        // Setup
        [SetUp]
        public void Setup()
        {
            panel = DeckStatsPanel.CreateDeckStatsPanel();
        }


        // Test creates deck stats panel
        [Test]
        public void CreatesDeckStatsPanel()
        {
            Assert.IsNotNull(panel);
        }


        // Test initializes
        [Test]
        public void Initializes()
        {
            Assert.IsTrue(panel.initialized);
        }


        // Test sets card counts labels
        [Test]
        public void SetsCardCountLabel_Deck()
        {
            panel.SetDeckCountLabel(1);

            Assert.AreEqual(1, int.Parse(panel.deckCountLabel.text));
        }

        [Test]
        public void SetsCardCountLabel_Building()
        {
            panel.SetCountLabel(CardType.Building, 1);

            Assert.AreEqual(1, int.Parse(panel.buildingCountLabel.text));
        }

        [Test]
        public void SetsCardCountLabel_Resource()
        {
            panel.SetCountLabel(CardType.Resource, 1);

            Assert.AreEqual(1, int.Parse(panel.resourceCountLabel.text));
        }

        [Test]
        public void SetsCardCountLabel_Unit()
        {
            panel.SetCountLabel(CardType.Unit, 1);

            Assert.AreEqual(1, int.Parse(panel.unitCountLabel.text));
        }


        // Test sets resources provided labels
        [Test]
        public void SetsResourceProvidedLabel_Bone()
        {
            panel.SetResourceProvidedLabel(ResourceType.Bone, 1);

            Assert.AreEqual(1, int.Parse(panel.boneHaveLabel.text));
        }

        [Test]
        public void SetsResourceProvidedLabel_Corpse()
        {
            panel.SetResourceProvidedLabel(ResourceType.Corpse, 1);

            Assert.AreEqual(1, int.Parse(panel.corpseHaveLabel.text));
        }

        [Test]
        public void SetsResourceProvidedLabel_Mana()
        {
            panel.SetResourceProvidedLabel(ResourceType.Mana, 1);

            Assert.AreEqual(1, int.Parse(panel.manaHaveLabel.text));
        }

        [Test]
        public void SetsResourceProvidedLabel_Stone()
        {
            panel.SetResourceProvidedLabel(ResourceType.Stone, 1);

            Assert.AreEqual(1, int.Parse(panel.stoneHaveLabel.text));
        }

        [Test]
        public void SetsResourceProvidedLabel_Wood()
        {
            panel.SetResourceProvidedLabel(ResourceType.Wood, 1);

            Assert.AreEqual(1, int.Parse(panel.woodHaveLabel.text));
        }


        // Test sets resources cost labels
        [Test]
        public void SetsResourceCostLabel_Bone()
        {
            panel.SetResourceCostLabel(ResourceType.Bone, 1);

            Assert.AreEqual(1, int.Parse(panel.boneCostLabel.text));
        }

        [Test]
        public void SetsResourceCostLabel_Corpse()
        {
            panel.SetResourceCostLabel(ResourceType.Corpse, 1);

            Assert.AreEqual(1, int.Parse(panel.corpseCostLabel.text));
        }

        [Test]
        public void SetsResourceCostLabel_Mana()
        {
            panel.SetResourceCostLabel(ResourceType.Mana, 1);

            Assert.AreEqual(1, int.Parse(panel.manaCostLabel.text));
        }

        [Test]
        public void SetsResourceCostLabel_Stone()
        {
            panel.SetResourceCostLabel(ResourceType.Stone, 1);

            Assert.AreEqual(1, int.Parse(panel.stoneCostLabel.text));
        }

        [Test]
        public void SetsResourceCostLabel_Wood()
        {
            panel.SetResourceCostLabel(ResourceType.Wood, 1);

            Assert.AreEqual(1, int.Parse(panel.woodCostLabel.text));
        }
    }
}