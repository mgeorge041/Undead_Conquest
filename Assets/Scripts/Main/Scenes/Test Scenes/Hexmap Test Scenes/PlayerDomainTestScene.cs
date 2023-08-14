using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlayerDomainTestScene : MonoBehaviour
{
    public Hexmap hexmap;
    private Building building;
    List<Hex> domainHexes = new List<Hex>();
    public List<LineRenderer> edgeLines { get; protected set; } = new List<LineRenderer>();


    public void StartScene()
    {
        hexmap.Initialize();

        building = Piece.CreatePiece<Building>(CardPaths.testBuilding);
        hexmap.hexmapData.AddPiece(building, Vector3Int.zero);

        SetDomainHexes();
        CreateHexListEdgeLines(HexListType.Move, domainHexes);
    }


    public void ResetScene()
    {
        hexmap.Reset();
    }


    private void SetDomainHexes()
    {
        List<Hex> buildingDomainHexes = HexPathPattern.GetHexesInRange(building.hex, building.buildingData.domainRange);
        foreach (Hex domainHex in buildingDomainHexes)
        {
            if (!domainHexes.Contains(domainHex))
                domainHexes.Add(domainHex);
        }
    }


    // Create edge lines
    protected void CreateHexListEdgeLines(HexListType hexListType, List<Hex> hexList)
    {
        List<List<Vector3>> edgePoints = PathEdge.GetPathEdge(hexList);
        foreach (List<Vector3> linePoints in edgePoints)
        {
            LineRenderer line = PathEdge.CreateEdgeLine(hexListType);
            edgeLines.Add(line);
            PathEdge.DisplayEdgeLine(line, linePoints);
        }
    }


    // Clear hex list edge lines
    protected void ClearHexListEdgeLines()
    {
        foreach (LineRenderer line in edgeLines)
        {
            Object.Destroy(line.gameObject);
        }
        edgeLines.Clear();
    }


    // Start is called before the first frame update
    void Start()
    {
        StartScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
