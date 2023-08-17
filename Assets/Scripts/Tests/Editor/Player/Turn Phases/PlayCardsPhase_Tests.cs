using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayerTests.TurnPhaseTests
{
    public class PlayCardsPhase_Tests
    {
        private PlayCardsPhase phase;
        private PlayerItemManager itemManager;
        private int numEndPhaseEvents;
        private TurnPhaseType nextPhase;
        private Card card;
        private HexmapData hexmapData;
        private Building building;


        // Setup
        [SetUp]
        public void Setup()
        {
            // Set resources
            itemManager = new PlayerItemManager();
            itemManager.resourceManager.SetResource(ResourceType.Bone, 5);
            itemManager.resourceManager.SetResource(ResourceType.Stone, 5);

            // Create card in hand
            card = Card.CreateCard(CardPaths.testUnit);
            itemManager.deck.AddNewCardToDeck(card);
            itemManager.DrawCard();
            
            // Events
            numEndPhaseEvents = 0;
            nextPhase = TurnPhaseType.None;

            // Add building
            hexmapData = new HexmapData();
            building = Piece.CreatePiece<Building>(CardPaths.testBuilding);
            hexmapData.AddPiece(building, Vector3Int.zero);
            itemManager.pieceManager.AddPiece(building);

            // Start phase
            phase = new PlayCardsPhase(itemManager);
            phase.StartPhase();
        }


        // Test creates play cards phase
        [Test]
        public void CreatesPlayCardsPhase()
        {
            Assert.IsNotNull(phase);
            Assert.AreEqual(TurnPhaseType.PlayCards, phase.phaseType);
        }


        // Test sets domain hexes
        [Test]
        public void StartsPhase()
        {
            Assert.AreEqual(18, phase.playableHexes.Count);
        }


        // Test clicking card
        [Test]
        public void SetsPlayableCard()
        {
            card.OnPointerClick(null);
            Assert.AreEqual(18, phase.playableHexes.Count);
            Assert.AreEqual(2, phase.edgeLines.Count);
        }

        [Test]
        public void SetsNullCard()
        {
            phase.SetSelectedCard(null);
            Assert.AreEqual(0, phase.playableHexes.Count);
            Assert.IsFalse(phase.hoverLine.gameObject.activeSelf);
            Assert.AreEqual(0, phase.edgeLines.Count);
        }

        [Test]
        public void SetsPlayableThenNullCard()
        {
            card.OnPointerClick(null);
            phase.SetSelectedCard(null);
            Assert.AreEqual(0, phase.playableHexes.Count);
            Assert.IsFalse(phase.hoverLine.gameObject.activeSelf);
            Assert.AreEqual(2, phase.edgeLines.Count);
            Assert.IsFalse(phase.edgeLines[0].gameObject.activeSelf);
            Assert.IsFalse(phase.edgeLines[1].gameObject.activeSelf);
        }

        [Test]
        public void SetsPlayableCardThenSameCard()
        {
            card.OnPointerClick(null);
            card.OnPointerClick(null);
            Assert.AreEqual(0, phase.playableHexes.Count);
            Assert.AreEqual(2, phase.edgeLines.Count);
            Assert.IsFalse(phase.edgeLines[0].gameObject.activeSelf);
            Assert.IsFalse(phase.edgeLines[1].gameObject.activeSelf);
        }


        // Test hover
        [Test]
        public void HoversAfterSettingPlayableCard_NullHex()
        {
            card.OnPointerClick(null);
            phase.Hover(null);

            Assert.IsFalse(phase.hoverLine.gameObject.activeSelf);
            Assert.AreEqual(0, phase.hoverLine.positionCount);
        }

        [Test]
        public void HoversAfterSettingPlayableCard_NonDomainHex()
        {
            Hex hex = hexmapData.GetHexAtHexCoords(Vector3Int.zero);
            card.OnPointerClick(null);
            phase.Hover(hex);

            Assert.IsFalse(phase.hoverLine.gameObject.activeSelf);
            Assert.AreEqual(0, phase.hoverLine.positionCount);
        }

        [Test]
        public void HoversAfterSettingPlayableCard_DomainHex()
        {
            Hex hex = hexmapData.GetHexAtHexCoords(new Vector3Int(1, -1, 0));
            card.OnPointerClick(null);
            phase.Hover(hex);

            Assert.IsTrue(phase.hoverLine.gameObject.activeSelf);
            Assert.AreEqual(6, phase.hoverLine.positionCount);
        }


        // Test ends phase
        private void HandleEndPhaseEvent(TurnPhaseType nextPhase)
        {
            this.nextPhase = nextPhase;
            numEndPhaseEvents++;
        }

        [Test]
        public void EndsPhase()
        {
            phase.eventManager.onEndPhase.Subscribe(HandleEndPhaseEvent);
            phase.EndPhase();
            Assert.AreEqual(1, numEndPhaseEvents);
            Assert.AreEqual(TurnPhaseType.MapAction, nextPhase);
        }

    }
}