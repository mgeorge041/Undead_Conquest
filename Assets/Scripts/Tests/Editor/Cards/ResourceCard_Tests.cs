using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CardTests
{
    public class ResourceCard_Tests
    {
        private ResourceCard card;


        // Setup
        [SetUp]
        public void Setup()
        {
            card = ResourceCard.CreateResourceCard();
        }


        // Test creates resource card
        [Test]
        public void CreatesResourceCard()
        {
            Assert.IsNotNull(card);
        }
    }
}