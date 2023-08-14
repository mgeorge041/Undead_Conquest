using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PieceTests
{
    public class Building_Tests
    {
        private Building building;
        private int numChangeHealthEvents;
        private int changeHealthEvent_CurrentHealth;
        private int changeHealthEvent_Health;


        // Setup
        [SetUp]
        public void Setup()
        {
            building = Building.CreateBuilding();

            numChangeHealthEvents = 0;
            changeHealthEvent_CurrentHealth = 0;
            changeHealthEvent_Health = 0;
        }


        // Test creates building
        [Test]
        public void CreatesBuilding()
        {
            Assert.IsNotNull(building);
        }

        [Test]
        public void CreatesBuildingWithCardInfo()
        {
            BuildingCardInfo cardInfo = CardInfo.LoadCardInfo<BuildingCardInfo>(CardPaths.testBuilding);
            building = Building.CreateBuilding(cardInfo);
            Assert.IsNotNull(building);
            Assert.AreEqual("Test Building", building.buildingCardInfo.cardName);
            Assert.AreEqual(10, building.buildingData.health);
        }


        // Test end turn
        [Test]
        public void EndsTurn()
        {
            building = Building.CreateBuilding(CardPaths.testBuilding);
            building.buildingData.SetHasActions(false);
            building.EndTurn();

            Assert.IsTrue(building.buildingData.hasActions);
        }


        // Test changes in health
        private void HandleChangeHealthEvent(int currentHealth, int health)
        {
            numChangeHealthEvents++;
            changeHealthEvent_CurrentHealth = currentHealth;
            changeHealthEvent_Health = health;
        }

        [Test]
        public void FiresEvent_ChangeHealth()
        {
            building = Building.CreateBuilding(CardPaths.testBuilding);
            building.buildingEventManager.onChangeHealth.Subscribe(HandleChangeHealthEvent);
            building.TakeDamage(1);

            Assert.AreEqual(1, numChangeHealthEvents);
            Assert.AreEqual(9, changeHealthEvent_CurrentHealth);
            Assert.AreEqual(10, changeHealthEvent_Health);
        }
    }
}