using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;

namespace PlayerTests.PlayerUITests
{
    public class PieceInfoPanel_Tests
    {
        private PieceInfoPanel panel;


        // Setup
        [SetUp]
        public void Setup()
        {
            panel = PieceInfoPanel.CreatePieceInfoPanel();
        }


        // Test creates piece info panel
        [Test]
        public void CreatesPieceInfoPanel()
        {
            Assert.IsNotNull(panel);
        }


        // Test initializes
        [Test]
        public void Initializes()
        {
            Assert.AreEqual(AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/Outline Material.mat").GetType(), panel.attackIcon.material.GetType());
            Assert.IsTrue(panel.initialized);
        }


        // Test setting piece
        [Test]
        public void SetsPiece_NullPiece()
        {
            panel.SetPiece(null);
            Assert.IsTrue(panel.gameObject.activeSelf);
        }

        [Test]
        public void SetsPiece_Unit()
        {
            Unit unit = Unit.CreateUnit(CardPaths.testUnit);
            panel.SetPiece(unit);

            Assert.AreEqual("Test Unit", panel.pieceNameLabel.text);
            Assert.AreEqual(unit.playableCardInfo.cardSprite, panel.pieceArt.sprite);
            Assert.AreEqual(Color.green, panel.attackIcon.material.GetColor("Outline_Color"));
            Assert.AreEqual(2, int.Parse(panel.attackLabel.text));
            Assert.AreEqual(1, int.Parse(panel.defenseLabel.text));
            Assert.AreEqual(1, int.Parse(panel.rangeLabel.text));
            Assert.AreEqual(1, int.Parse(panel.sightLabel.text));
            Assert.AreEqual("5/5", panel.healthLabel.text);
            Assert.AreEqual("3/3", panel.speedLabel.text);
            Assert.AreEqual("0/0", panel.manaLabel.text);
        }

        [Test]
        public void SetsPiece_Building()
        {
            Building building = Building.CreateBuilding(CardPaths.testBuilding);
            panel.SetPiece(building);

            Assert.AreEqual("Test Building", panel.pieceNameLabel.text);
            Assert.AreEqual(building.playableCardInfo.cardSprite, panel.pieceArt.sprite);
            Assert.AreEqual(Color.green, panel.attackIcon.material.GetColor("Outline_Color"));
            Assert.AreEqual(0, int.Parse(panel.attackLabel.text));
            Assert.AreEqual(0, int.Parse(panel.defenseLabel.text));
            Assert.AreEqual(0, int.Parse(panel.rangeLabel.text));
            Assert.AreEqual(0, int.Parse(panel.sightLabel.text));
            Assert.AreEqual("10/10", panel.healthLabel.text);
            Assert.AreEqual("0/0", panel.speedLabel.text);
            Assert.AreEqual("0/0", panel.manaLabel.text);
        }

        [Test]
        public void SetsPiece_SamePiece()
        {
            Unit unit = Unit.CreateUnit(CardPaths.testUnit);
            panel.SetPiece(unit);
            panel.SetPiece(unit);
            Assert.IsTrue(panel.gameObject.activeSelf);
        }

        [Test]
        public void SetsUnit_NoActions()
        {
            Unit unit = Unit.CreateUnit(CardPaths.testUnit);
            unit.unitData.SetHasActions(false);
            panel.SetPiece(unit);

            Assert.AreEqual(0, panel.attackIcon.material.GetFloat("Outside"));
        }


        // Test resets
        [Test]
        public void Resets()
        {
            panel.Reset();
            Assert.AreEqual("", panel.pieceNameLabel.text);
            Assert.IsNull(panel.pieceArt.sprite);
            Assert.AreEqual(0, int.Parse(panel.attackLabel.text));
            Assert.AreEqual(0, int.Parse(panel.defenseLabel.text));
            Assert.AreEqual(0, int.Parse(panel.rangeLabel.text));
            Assert.AreEqual(0, int.Parse(panel.sightLabel.text));
            Assert.AreEqual("0/0", panel.healthLabel.text);
            Assert.AreEqual("0/0", panel.speedLabel.text);
            Assert.AreEqual("0/0", panel.manaLabel.text);
        }
    }
}