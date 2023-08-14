using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayerTests.PlayerUITests
{
    public class Fillbar_Tests
    {
        private Fillbar fillbar;


        // Setup
        [SetUp]
        public void Setup()
        {
            fillbar = Fillbar.CreateFillbar();
        }


        // Test creates fillbar
        [Test]
        public void CreatesFillbar()
        {
            Assert.IsNotNull(fillbar);
        }


        // Test setting fillbar fill
        [Test]
        public void SetsFill_0()
        {
            float fillAmount = 0;
            fillbar.SetFill(fillAmount);
            Assert.AreEqual(fillAmount, fillbar.fill);
        }

        [Test]
        public void SetsFill_Negative()
        {
            float fillAmount = -1;
            fillbar.SetFill(fillAmount);
            Assert.AreEqual(0, fillbar.fill);
        }

        [Test]
        public void SetsFill_GreaterThan1()
        {
            float fillAmount = 2;
            fillbar.SetFill(fillAmount);
            Assert.AreEqual(1, fillbar.fill);
        }

        [Test]
        public void SetsFill_ValidFill()
        {
            float fillAmount = 0.5f;
            fillbar.SetFill(fillAmount);
            Assert.AreEqual(fillAmount, fillbar.fill);
        }
    }
}