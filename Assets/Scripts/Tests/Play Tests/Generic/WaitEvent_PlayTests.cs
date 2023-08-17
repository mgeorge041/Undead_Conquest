using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace GenericTests
{
    public class WaitEvent_PlayTests
    {
        private WaitEvent waitEvent;
        private int numWaitEvents;


        // Setup
        [SetUp]
        public void Setup()
        {
            numWaitEvents = 0;
        }

        // Test fires function at end of wait
        private void HandleFinishWaitEvent()
        {
            numWaitEvents++;
        }

        [UnityTest]
        public IEnumerator WaitsFor0Seconds()
        {
            float wait = 0;
            waitEvent = WaitEvent.CreateWaitEvent(wait, () => HandleFinishWaitEvent());
            waitEvent.StartWait();
            yield return new WaitForSeconds(wait);
            Assert.AreEqual(1, numWaitEvents);
        }

        [UnityTest]
        public IEnumerator WaitsFor1Second()
        {
            float wait = 1;
            waitEvent = WaitEvent.CreateWaitEvent(wait, () => HandleFinishWaitEvent());
            waitEvent.StartWait();
            yield return new WaitForSeconds(wait);
            Assert.AreEqual(1, numWaitEvents);
        }
    }
}