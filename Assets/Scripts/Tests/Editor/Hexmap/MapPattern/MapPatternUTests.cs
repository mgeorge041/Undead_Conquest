using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Map.Pattern.UTests
{
    public class MapPatternUTests
    {
        private List<Vector3Int> hexCoords;


        // Setup
        [SetUp]
        public void Setup()
        {
            hexCoords = new List<Vector3Int>();
        }
    }
}