using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayerTests
{
    public class PlayerCamera_Tests
    {
        private PlayerCamera playerCamera;


        // Setup
        [SetUp]
        public void Setup()
        {
            playerCamera = PlayerCamera.InstantiatePlayerCamera();
        }


        // Teardown
        [TearDown]
        public void Teardown()
        {
            C.Destroy(playerCamera);
        }


        // Test creates camera
        [Test]
        public void InstantiatesPlayerCamera()
        {
            Assert.IsNotNull(playerCamera);
        }


        // Test sets orthographic size on creation
        [Test]
        public void SetsOrthographicSize_Creation()
        {
            Assert.AreEqual(1.8f, playerCamera.cameraObject.orthographicSize);
        }


        // Sets scale
        [Test]
        public void SetsScale_2()
        {
            playerCamera.SetScale(2);
            Assert.AreEqual(2, playerCamera.GetScale());
        }

        [Test]
        public void SetsScale_Max3()
        {
            playerCamera.SetScale(4);
            Assert.AreEqual(3, playerCamera.GetScale());
        }

        [Test]
        public void SetsScale_Min1()
        {
            playerCamera.SetScale(0);
            Assert.AreEqual(1, playerCamera.GetScale());
        }


        // Test zoom
        [Test]
        public void ZoomsIn_Scale1to2()
        {
            playerCamera.ZoomCamera(1);
            Assert.AreEqual(0.9f, playerCamera.cameraObject.orthographicSize);
            Assert.AreEqual(2, playerCamera.GetScale());
        }

        [Test]
        public void ZoomsIn_MaxScale3()
        {
            playerCamera.ZoomCamera(1);
            playerCamera.ZoomCamera(1);
            playerCamera.ZoomCamera(1);
            Assert.AreEqual(0.6f, playerCamera.cameraObject.orthographicSize);
            Assert.AreEqual(3, playerCamera.GetScale());
        }

        [Test]
        public void ZoomsOut_Scale2to1()
        {
            playerCamera.SetScale(2);
            playerCamera.ZoomCamera(-1);
            Assert.AreEqual(1, playerCamera.GetScale());
            Assert.AreEqual(1.8f, playerCamera.cameraObject.orthographicSize);
        }

        [Test]
        public void ZoomsOut_MinScale1()
        {
            playerCamera.ZoomCamera(-1);
            Assert.AreEqual(1, playerCamera.GetScale());
            Assert.AreEqual(1.8f, playerCamera.cameraObject.orthographicSize);
        }


        // Test calculating camera bounds for hexagon map
        [Test]
        public void CalculatesBounds_Scale1_HexagonPattern_Radius5()
        {
            HexagonMapPattern mapPattern = new HexagonMapPattern(5);
            playerCamera.SetMapPattern(mapPattern);
            PlayerCamera.CameraBounds expectedBounds = new PlayerCamera.CameraBounds(0, 0, -0.28f, 0.28f);
            Assert.IsTrue(playerCamera.cameraBounds.Equals(expectedBounds));
        }

        [Test]
        public void CalculatesBounds_Scale2_HexagonPattern_Radius5()
        {
            playerCamera.SetScale(2);
            HexagonMapPattern mapPattern = new HexagonMapPattern(5);
            playerCamera.SetMapPattern(mapPattern);
            PlayerCamera.CameraBounds expectedBounds = new PlayerCamera.CameraBounds(-0.08f, 0.08f, -1.18f, 1.18f); Debug.Log(playerCamera.cameraBounds);
            Assert.IsTrue(playerCamera.cameraBounds.Equals(expectedBounds));
        }

        [Test]
        public void CalculatesBounds_Scale1_HexagonPattern_Radius10()
        {
            HexagonMapPattern mapPattern = new HexagonMapPattern(10);
            playerCamera.SetMapPattern(mapPattern);
            PlayerCamera.CameraBounds expectedBounds = new PlayerCamera.CameraBounds(0, 0, -1.88f, 1.88f);
            Assert.IsTrue(playerCamera.cameraBounds.Equals(expectedBounds));
        }

        [Test]
        public void CalculatesBounds_Scale2_HexagonPattern_Radius10()
        {
            playerCamera.SetScale(2);
            HexagonMapPattern mapPattern = new HexagonMapPattern(10);
            playerCamera.SetMapPattern(mapPattern);
            PlayerCamera.CameraBounds expectedBounds = new PlayerCamera.CameraBounds(-1.28f, 1.28f, -2.78f, 2.78f);
            Assert.IsTrue(playerCamera.cameraBounds.Equals(expectedBounds));
        }


        // Test calculating camera bounds for diamond map
        [Test]
        public void CalculatesBounds_Scale1_DiamondPattern_5x5()
        {
            DiamondMapPattern mapPattern = new DiamondMapPattern(5, 5);
            playerCamera.SetMapPattern(mapPattern);
            PlayerCamera.CameraBounds expectedBounds = new PlayerCamera.CameraBounds(0, 0, 0, 0);
            Assert.IsTrue(playerCamera.cameraBounds.Equals(expectedBounds));
        }

        [Test]
        public void CalculatesBounds_Scale2_DiamondPattern_5x5()
        {
            playerCamera.SetScale(2);
            DiamondMapPattern mapPattern = new DiamondMapPattern(5, 5);
            playerCamera.SetMapPattern(mapPattern);
            PlayerCamera.CameraBounds expectedBounds = new PlayerCamera.CameraBounds(0, 0, -0.54f, 0.54f);
            Assert.IsTrue(playerCamera.cameraBounds.Equals(expectedBounds));
        }

        [Test]
        public void CalculatesBounds_Scale1_DiamondPattern_10x10()
        {
            DiamondMapPattern mapPattern = new DiamondMapPattern(10, 10);
            playerCamera.SetMapPattern(mapPattern);
            PlayerCamera.CameraBounds expectedBounds = new PlayerCamera.CameraBounds(0, 0, -0.84f, 0.84f);
            Assert.IsTrue(playerCamera.cameraBounds.Equals(expectedBounds));
        }

        [Test]
        public void CalculatesBounds_Scale2_DiamondPattern_10x10()
        {
            playerCamera.SetScale(2);
            DiamondMapPattern mapPattern = new DiamondMapPattern(10, 10);
            playerCamera.SetMapPattern(mapPattern);
            PlayerCamera.CameraBounds expectedBounds = new PlayerCamera.CameraBounds(0, 0, -1.74f, 1.74f);
            Assert.IsTrue(playerCamera.cameraBounds.Equals(expectedBounds));
        }


        // Test calculating camera bounds for rectangular map
        [Test]
        public void CalculatesBounds_Scale1_RectanglePattern_5x5()
        {
            RectangleMapPattern mapPattern = new RectangleMapPattern(5, 5);
            playerCamera.SetMapPattern(mapPattern);
            PlayerCamera.CameraBounds expectedBounds = new PlayerCamera.CameraBounds(0, 0, 0, 0);
            Assert.IsTrue(playerCamera.cameraBounds.Equals(expectedBounds));
        }

        [Test]
        public void CalculatesBounds_Scale2_RectanglePattern_5x5()
        {
            playerCamera.SetScale(2);
            RectangleMapPattern mapPattern = new RectangleMapPattern(5, 5);
            playerCamera.SetMapPattern(mapPattern);
            PlayerCamera.CameraBounds expectedBounds = new PlayerCamera.CameraBounds(0, 0, -0.22f, 0.22f);
            Assert.IsTrue(playerCamera.cameraBounds.Equals(expectedBounds));
        }

        [Test]
        public void CalculatesBounds_Scale1_RectanglePattern_10x10()
        {
            RectangleMapPattern mapPattern = new RectangleMapPattern(10, 10);
            playerCamera.SetMapPattern(mapPattern);
            PlayerCamera.CameraBounds expectedBounds = new PlayerCamera.CameraBounds(0, 0, -0.12f, 0.12f);
            Assert.IsTrue(playerCamera.cameraBounds.Equals(expectedBounds));
        }

        [Test]
        public void CalculatesBounds_Scale2_RectanglePattern_10x10()
        {
            playerCamera.SetScale(2);
            RectangleMapPattern mapPattern = new RectangleMapPattern(10, 10);
            playerCamera.SetMapPattern(mapPattern);
            PlayerCamera.CameraBounds expectedBounds = new PlayerCamera.CameraBounds(0, 0, -1.02f, 1.02f);
            Assert.IsTrue(playerCamera.cameraBounds.Equals(expectedBounds));
        }
    }
}