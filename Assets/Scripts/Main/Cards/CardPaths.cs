using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardPaths
{
    private const string cardsPath = "Cards/";

    // Test cards
    private const string testPath = cardsPath + "Test Cards/";
    public const string testBoneResource = testPath + "Test Bone Resource";
    public const string testUnit = testPath + "Test Unit";
    public const string testBuilding = testPath + "Test Building";
    public const string testBuildingResourceStartAction = testPath + "Test Building Resource Start Action";


    // Resources
    private const string resourcesPath = cardsPath + "Resource Cards/";
    public const string bone = resourcesPath + "Bone Resource";
    public const string corpse = resourcesPath + "Corpse Resource";
    public const string mana = resourcesPath + "Mana Resource";
    public const string stone = resourcesPath + "Stone Resource";
    public const string wood = resourcesPath + "Wood Resource";


    // Units
    private const string unitsPath = cardsPath + "Unit Cards/";
    public const string bagOfBones = unitsPath + "Bag of Bones";
    public const string demonHound = unitsPath + "Demon Hound";
    public const string ghoul = unitsPath + "Ghoul";
    public const string skeleton = unitsPath + "Skeleton";
    public const string zombie = unitsPath + "Zombie";


    // Buildings
    private const string buildingsPath = cardsPath + "Building Cards/";
    public const string mainBase = buildingsPath + "Main Base";
    public const string humanTrap = buildingsPath + "Human Trap";
}
