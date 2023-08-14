using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CardTests
{
    public class UnitCard_Tests
    {
        private UnitCard card;


        // Setup
        [SetUp]
        public void Setup()
        {
            card = UnitCard.CreateUnitCard();
        }


        // Test creates unit card
        [Test]
        public void CreatesUnitCard()
        {
            Assert.IsNotNull(card);
        }
    }
}