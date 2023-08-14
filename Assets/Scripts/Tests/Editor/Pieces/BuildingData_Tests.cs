using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PieceTests
{
    public class BuildingData_Tests
    {
        private BuildingData data;


        // Setup
        [SetUp]
        public void Setup()
        {
            data = new BuildingData();
        }


        // Test creates building data
        [Test]
        public void CreatesBuildingData()
        {
            Assert.IsNotNull(data);
        }

        [Test]
        public void CreatesBuildingData_WithBuildingCardInfo()
        {
            BuildingCardInfo cardInfo = CardInfo.LoadCardInfo<BuildingCardInfo>(CardPaths.testBuilding);
            data = new BuildingData(cardInfo);

            Assert.IsNotNull(data);
            Assert.AreEqual(10, data.health);
            Assert.AreEqual(10, data.currentHealth);
            Assert.AreEqual(0, data.attack);
            Assert.AreEqual(0, data.defense);
            Assert.AreEqual(0, data.mana);
            Assert.AreEqual(0, data.range);
            Assert.AreEqual(2, data.domainRange);
        }

        [Test]
        public void CreatesBuildingData_Copy()
        {
            BuildingCardInfo cardInfo = CardInfo.LoadCardInfo<BuildingCardInfo>(CardPaths.testBuilding);
            data = new BuildingData(cardInfo);
            data.SetStat(PieceStatType.CurrentHealth, 1);

            BuildingData newData = new BuildingData(data);

            Assert.IsNotNull(newData);
            Assert.AreEqual(data.buildingCardInfo, newData.buildingCardInfo);
            Assert.AreEqual(10, newData.health);
            Assert.AreEqual(1, newData.currentHealth);
            Assert.AreEqual(0, newData.attack);
            Assert.AreEqual(0, newData.defense);
            Assert.AreEqual(0, newData.mana);
            Assert.AreEqual(0, newData.range);
            Assert.AreEqual(2, newData.domainRange);
            Assert.AreEqual(data.hasActions, newData.hasActions);
        }


        // Test getting stats
        [Test]
        public void GetsStats()
        {
            Assert.AreEqual(0, data.GetStat(PieceStatType.Attack));
            Assert.AreEqual(0, data.GetStat(PieceStatType.Defense));
            Assert.AreEqual(0, data.GetStat(PieceStatType.Health));
            Assert.AreEqual(0, data.GetStat(PieceStatType.CurrentHealth));
            Assert.AreEqual(0, data.GetStat(PieceStatType.Mana));
            Assert.AreEqual(0, data.GetStat(PieceStatType.Range));
        }

        [Test]
        public void GetsStats_WithBuildingCardInfo()
        {
            BuildingCardInfo cardInfo = CardInfo.LoadCardInfo<BuildingCardInfo>(CardPaths.testBuilding);
            data = new BuildingData(cardInfo);

            Assert.AreEqual(0, data.GetStat(PieceStatType.Attack));
            Assert.AreEqual(0, data.GetStat(PieceStatType.Defense));
            Assert.AreEqual(10, data.GetStat(PieceStatType.Health));
            Assert.AreEqual(10, data.GetStat(PieceStatType.CurrentHealth));
            Assert.AreEqual(0, data.GetStat(PieceStatType.Mana));
            Assert.AreEqual(0, data.GetStat(PieceStatType.Range));
            Assert.AreEqual(2, data.domainRange);
        }


        // Test setting stats
        [Test]
        public void SetsStats()
        {
            int amount = 1;
            data.SetStat(PieceStatType.Attack, amount);
            Assert.AreEqual(amount, data.GetStat(PieceStatType.Attack));
            Assert.AreEqual(amount, data.attack);
        }

        [Test]
        public void SetsStats_Negative()
        {
            int amount = -1;
            data.SetStat(PieceStatType.Attack, amount);
            Assert.AreEqual(0, data.GetStat(PieceStatType.Attack));
            Assert.AreEqual(0, data.attack);
        }


        // Test adding stats
        [Test]
        public void AddsStats()
        {
            int amount = 1;
            data.AddStat(PieceStatType.Attack, amount);
            Assert.AreEqual(amount, data.GetStat(PieceStatType.Attack));
            Assert.AreEqual(amount, data.attack);
        }

        [Test]
        public void AddsStats_Below0()
        {
            int amount = -1;
            data.AddStat(PieceStatType.Attack, amount);
            Assert.AreEqual(0, data.GetStat(PieceStatType.Attack));
            Assert.AreEqual(0, data.attack);
        }
    }
}