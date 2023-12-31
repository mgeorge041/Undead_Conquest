using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace GenericTests
{
    public class GameEvent_Tests
    {
        private GameEvent<int> gameEvent;
        private int numGameEvents;
        private int gameEventInfo;


        // Setup
        [SetUp]
        public void Setup()
        {
            gameEvent = new GameEvent<int>();
            numGameEvents = 0;
            gameEventInfo = 0;
        }


        // Test creates game event
        [Test]
        public void CreatesGameEvent()
        {
            Assert.IsNotNull(gameEvent);
        }


        // Test fires game event
        private void HandleGameEvent(int gameEventInfo)
        {
            numGameEvents++;
            this.gameEventInfo = gameEventInfo;
        }

        [Test]
        public void FiresEvent()
        {
            gameEvent.Subscribe(HandleGameEvent);
            gameEvent.OnEvent(1);

            Assert.AreEqual(1, numGameEvents);
            Assert.AreEqual(1, gameEventInfo);
        }


        // Test gets number of subscriptions
        [Test]
        public void GetsNumberOfSubscriptions_0()
        {
            int numSubscriptions = gameEvent.count;
            Assert.AreEqual(0, numSubscriptions);
        }

        [Test]
        public void GetsNumberOfSubscriptions_1()
        {
            gameEvent.Subscribe(HandleGameEvent);
            int numSubscriptions = gameEvent.count;
            Assert.AreEqual(1, numSubscriptions);
        }

        [Test]
        public void GetsNumberOfSubscriptions_1Then0()
        {
            gameEvent.Subscribe(HandleGameEvent);
            gameEvent.Unsubscribe(HandleGameEvent);
            int numSubscriptions = gameEvent.count;
            Assert.AreEqual(0, numSubscriptions);
        }

    }
}