using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayerTests.InputTests
{
    public class PlayerInputController_Tests
    {
        private PlayerInputController inputController;
        private int numLeftClickEvents;
        private int numRightClickEvents;
        private int numHoverEvents;
        private int numKeyboardArrowEvents;
        private int numScrollEvents;


        // Setup
        [SetUp]
        public void Setup()
        {
            inputController = PlayerInputController.CreatePlayerInputController();
            numLeftClickEvents = 0;
            numRightClickEvents = 0;
            numHoverEvents = 0;
            numKeyboardArrowEvents = 0;
            numScrollEvents = 0;
        }


        // Test creates input controller
        [Test]
        public void CreatesPlayerInputController()
        {
            Assert.IsNotNull(inputController);
        }


        // Test initialization
        [Test]
        public void Initializes()
        {
            Assert.AreEqual(typeof(EmptyPhaseControllerSettings), inputController.currentControllerSettings.GetType());
            Assert.IsTrue(inputController.initialized);
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
            inputController.settingsEventManager.onLeftClick.Subscribe(HandleLeftClick);
            inputController.currentControllerSettings.LeftClick(Vector3.one);
            Assert.AreEqual(1, numLeftClickEvents);
        }

        [Test]
        public void FiresEvent_RightClick()
        {
            inputController.settingsEventManager.onRightClick.Subscribe(HandleRightClick);
            inputController.currentControllerSettings.RightClick(Vector3.one);
            Assert.AreEqual(1, numRightClickEvents);
        }

        [Test]
        public void FiresEvent_Hover()
        {
            inputController.settingsEventManager.onHover.Subscribe(HandleHover);
            inputController.currentControllerSettings.Hover(Vector3.one);
            Assert.AreEqual(1, numHoverEvents);
        }

        [Test]
        public void FiresEvent_KeyboardArrows()
        {
            inputController.settingsEventManager.onPressKeyboardArrows.Subscribe(HandleKeyboardArrows);
            inputController.currentControllerSettings.PressKeyboardArrows(1, 1);
            Assert.AreEqual(1, numKeyboardArrowEvents);
        }

        [Test]
        public void FiresEvent_Scroll()
        {
            inputController.settingsEventManager.onScroll.Subscribe(HandleScroll);
            inputController.currentControllerSettings.Scroll(1);
            Assert.AreEqual(1, numScrollEvents);
        }


        // Test setting new input controller settings
        [Test]
        public void SetsCurrentInputController()
        {
            PlayCardsPhaseControllerSettings newInputController = new PlayCardsPhaseControllerSettings();
            inputController.SetCurrentControllerSettings(newInputController);
            Assert.AreEqual(typeof(PlayCardsPhaseControllerSettings), inputController.currentControllerSettings.GetType());
            Assert.AreEqual(newInputController.eventManager, inputController.settingsEventManager);
        }

        [Test]
        public void SetsCurrentInputController_LeftClick()
        {
            PlayCardsPhaseControllerSettings newInputController = new PlayCardsPhaseControllerSettings();
            inputController.SetCurrentControllerSettings(newInputController);
            inputController.settingsEventManager.onLeftClick.Subscribe(HandleLeftClick);
            inputController.currentControllerSettings.LeftClick(Vector3.one);

            Assert.AreEqual(1, numLeftClickEvents);
        }

        [Test]
        public void SetsCurrentInputController_RightClick()
        {
            PlayCardsPhaseControllerSettings newInputController = new PlayCardsPhaseControllerSettings();
            inputController.SetCurrentControllerSettings(newInputController);
            inputController.settingsEventManager.onRightClick.Subscribe(HandleRightClick);
            inputController.currentControllerSettings.RightClick(Vector3.one);
            
            Assert.AreEqual(1, numRightClickEvents);
        }

        [Test]
        public void SetsCurrentInputController_Hover()
        {
            PlayCardsPhaseControllerSettings newInputController = new PlayCardsPhaseControllerSettings();
            inputController.SetCurrentControllerSettings(newInputController);
            inputController.settingsEventManager.onHover.Subscribe(HandleHover);
            inputController.currentControllerSettings.Hover(Vector3.one);
            
            Assert.AreEqual(1, numHoverEvents);
        }

        [Test]
        public void SetsCurrentInputController_KeyboardArrows()
        {
            PlayCardsPhaseControllerSettings newInputController = new PlayCardsPhaseControllerSettings();
            inputController.SetCurrentControllerSettings(newInputController);
            inputController.settingsEventManager.onPressKeyboardArrows.Subscribe(HandleKeyboardArrows);
            inputController.currentControllerSettings.PressKeyboardArrows(1, 1);
            
            Assert.AreEqual(1, numKeyboardArrowEvents);
        }

        [Test]
        public void SetsCurrentInputController_Scroll()
        {
            PlayCardsPhaseControllerSettings newInputController = new PlayCardsPhaseControllerSettings();
            inputController.SetCurrentControllerSettings(newInputController);
            inputController.settingsEventManager.onScroll.Subscribe(HandleScroll);
            inputController.currentControllerSettings.Scroll(1);
            
            Assert.AreEqual(1, numScrollEvents);
        }
    }
}