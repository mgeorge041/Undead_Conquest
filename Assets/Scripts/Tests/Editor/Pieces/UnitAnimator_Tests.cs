using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PieceTests
{
    public class UnitAnimator_Tests
    {
        private Unit unit;
        private HexmapData hexmapData;


        // Setup
        [SetUp]
        public void Setup()
        {
            unit = Unit.CreateUnit(CardPaths.testUnit);
            hexmapData = new HexmapData();

            hexmapData.AddPiece(unit, Vector3Int.zero);
        }


        // 
    }
}