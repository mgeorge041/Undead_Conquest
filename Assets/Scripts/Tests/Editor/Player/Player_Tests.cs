using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayerTests
{
    public class Player_Tests
    {
        private Player player;


        // Setup
        [SetUp]
        public void Setup()
        {
            player = Player.CreatePlayer();
        }


        // Test creates player
        [Test]
        public void CreatesPlayer()
        {
            Assert.IsNotNull(player);
        }


        // Test initializes components
        [Test]
        public void InitializesComponents()
        {
            Assert.IsTrue(player.playerUI.initialized);
            Assert.IsTrue(player.initialized);
        }
    }
}