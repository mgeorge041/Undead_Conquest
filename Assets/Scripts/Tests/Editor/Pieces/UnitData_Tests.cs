using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PieceTests
{
    public class UnitData_Tests
    {
        private UnitData data;


        // Setup
        [SetUp]
        public void Setup()
        {
            data = new UnitData();
        }


        // Test creates unit data
        [Test]
        public void CreatesUnitData()
        {
            Assert.IsNotNull(data);
        }

        [Test]
        public void CreatesUnitData_WithUnitCardInfo()
        {
            UnitCardInfo cardInfo = CardInfo.LoadCardInfo<UnitCardInfo>(CardPaths.testUnit);
            data = new UnitData(cardInfo);
            
            Assert.IsNotNull(data);
            Assert.AreEqual(5, data.health);
            Assert.AreEqual(5, data.currentHealth);
            Assert.AreEqual(2, data.attack);
            Assert.AreEqual(1, data.defense);
            Assert.AreEqual(3, data.speed);
            Assert.AreEqual(3, data.currentSpeed);
            Assert.AreEqual(0, data.mana);
            Assert.AreEqual(1, data.range);
        }

        [Test]
        public void CreatesUnitData_Copy()
        {
            UnitCardInfo cardInfo = CardInfo.LoadCardInfo<UnitCardInfo>(CardPaths.testUnit);
            data = new UnitData(cardInfo);
            data.SetStat(PieceStatType.CurrentHealth, 1);
            data.SetStat(PieceStatType.CurrentSpeed, 1);

            UnitData newData = new UnitData(data);

            Assert.IsNotNull(newData);
            Assert.AreEqual(data.unitCardInfo, newData.unitCardInfo);
            Assert.AreEqual(5, newData.health);
            Assert.AreEqual(1, newData.currentHealth);
            Assert.AreEqual(2, newData.attack);
            Assert.AreEqual(1, newData.defense);
            Assert.AreEqual(3, newData.speed);
            Assert.AreEqual(1, newData.currentSpeed);
            Assert.AreEqual(0, newData.mana);
            Assert.AreEqual(1, newData.range);
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
            Assert.AreEqual(0, data.GetStat(PieceStatType.Speed));
            Assert.AreEqual(0, data.GetStat(PieceStatType.CurrentSpeed));
            Assert.AreEqual(0, data.GetStat(PieceStatType.Range));
        }

        [Test]
        public void GetsStats_WithUnitCardInfo()
        {
            UnitCardInfo cardInfo = CardInfo.LoadCardInfo<UnitCardInfo>(CardPaths.testUnit);
            data = new UnitData(cardInfo);

            Assert.AreEqual(2, data.GetStat(PieceStatType.Attack));
            Assert.AreEqual(1, data.GetStat(PieceStatType.Defense));
            Assert.AreEqual(5, data.GetStat(PieceStatType.Health));
            Assert.AreEqual(5, data.GetStat(PieceStatType.CurrentHealth));
            Assert.AreEqual(0, data.GetStat(PieceStatType.Mana));
            Assert.AreEqual(3, data.GetStat(PieceStatType.Speed));
            Assert.AreEqual(3, data.GetStat(PieceStatType.CurrentSpeed));
            Assert.AreEqual(1, data.GetStat(PieceStatType.Range));
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