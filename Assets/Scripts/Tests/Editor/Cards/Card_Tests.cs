using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CardTests
{
    public class Card_Tests
    {
        private Card card;


        // Setup
        [SetUp]
        public void Setup()
        {
            card = Card.CreateCard(CardPaths.testBoneResource);
        }


        // Test creates card
        [Test]
        public void CreatesCard()
        {
            Assert.IsNotNull(card);
        }
    }
}