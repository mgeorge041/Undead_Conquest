using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayerTests.InputTests
{
    public class PlayCardsPhaseControllerSettings_Tests
    {
        private PlayCardsPhaseControllerSettings settings;
        private int numLeftClickEvents;
        private int numRightClickEvents;
        private int numHoverEvents;
        private int numKeyboardArrowEvents;
        private int numScrollEvents;


        // Setup
        [SetUp]
        public void Setup()
        {
            settings = new PlayCardsPhaseControllerSettings();
            numLeftClickEvents = 0;
            numRightClickEvents = 0;
            numHoverEvents = 0;
            numKeyboardArrowEvents = 0;
            numScrollEvents = 0;
        }


        // Test creates play cards phase controller settings
        [Test]
        public void CreatesPlayCardsPhaseControllerSettings()
        {
            Assert.IsNotNull(settings);
        }


        // Test fires events
        private void HandleLeftClick(Vector3 mousePosition)
        {
            numLeftClickEvents++;
        }
        private void HandleRightClick(Vector3 mousePosition)
        {
            numRightClickEvents++;
        }
        private void HandleHover(Vector3 mousePosition)
        {
            numHoverEvents++;
        }
        private void HandleKeyboardArrows(float moveX, float moveY)
        {
            numKeyboardArrowEvents++;
        }
        private void HandleScroll(float moveZ)
        {
            numScrollEvents++;
        }

        [Test]
        public void FiresEvent_LeftClick()
        {
            settings.eventManager.onLeftClick.Subscribe(HandleLeftClick);
            settings.LeftClick(Vector3.one);
            Assert.AreEqual(1, numLeftClickEvents);
        }

        [Test]
        public void FiresEvent_RightClick()
        {
            settings.eventManager.onRightClick.Subscribe(HandleRightClick);
            settings.RightClick(Vector3.one);
            Assert.AreEqual(1, numRightClickEvents);
        }

        [Test]
        public void FiresEvent_Hover()
        {
            settings.eventManager.onHover.Subscribe(HandleHover);
            settings.Hover(Vector3.one);
            Assert.AreEqual(1, numHoverEvents);
        }

        [Test]
        public void FiresEvent_KeyboardArrows()
        {
            settings.eventManager.onPressKeyboardArrows.Subscribe(HandleKeyboardArrows);
            settings.PressKeyboardArrows(1, 1);
            Assert.AreEqual(1, numKeyboardArrowEvents);
        }

        [Test]
        public void FiresEvent_Scroll()
        {
            settings.eventManager.onScroll.Subscribe(HandleScroll);
            settings.Scroll(1);
            Assert.AreEqual(1, numScrollEvents);
        }
    }
}