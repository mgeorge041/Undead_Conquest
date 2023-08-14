using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayerTests.PlayerUITests
{
    public class PlayerUI_Tests
    {
        private PlayerUI ui;


        // Setup
        [SetUp]
        public void Setup()
        {
            ui = PlayerUI.CreatePlayerUI();
        }


        // Test creates player UI
        [Test]
        public void CreatesPlayerUI()
        {
            Assert.IsNotNull(ui);
        }


        // Test initializes components
        [Test]
        public void InitializesComponents()
        {
            Assert.IsTrue(ui.resourcePanel.initialized);
            Assert.IsTrue(ui.handPanel.initialized);
            Assert.IsTrue(ui.initialized);
        }
    }
}