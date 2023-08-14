using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PieceTests.StartActionTests
{
    public class StartAction_Tests
    {
        // Test create draw start action
        [Test]
        public void CreatesDrawStartAction()
        {
            DrawStartAction startAction = StartAction.CreateStartAction<DrawStartAction>(StartActionPaths.testDrawAction);
            Assert.IsNotNull(startAction);
        }


        // Test create resource start action
        [Test]
        public void CreatesResourceStartAction()
        {
            ResourceStartAction startAction = StartAction.CreateStartAction<ResourceStartAction>(StartActionPaths.testResourceAction);
            Assert.IsNotNull(startAction);
        }


        // Test create add card start action
        [Test]
        public void CreatesAddCardStartAction()
        {
            AddCardStartAction startAction = StartAction.CreateStartAction<AddCardStartAction>(StartActionPaths.testAddCardAction);
            Assert.IsNotNull(startAction);
        }
    }
}