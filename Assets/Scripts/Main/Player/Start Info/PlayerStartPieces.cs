using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Start Pieces", menuName = "Player Start Pieces")]
public class PlayerStartPieces : ScriptableObject
{
    public Dictionary<PlayableCardInfo, Vector3Int> pieces => GetPieces();
    public List<PlayableCardInfo> startPieces = new List<PlayableCardInfo>();
    public List<Vector3Int> startLocations = new List<Vector3Int>();


    // Load start pieces
    public static PlayerStartPieces LoadStartPieces(string startPiecesPath)
    {
        PlayerStartPieces startPieces = Instantiate(Resources.Load<PlayerStartPieces>(startPiecesPath));
        return startPieces;
    }


    // Get pieces and locations
    public Dictionary<PlayableCardInfo, Vector3Int> GetPieces()
    {
        Dictionary<PlayableCardInfo, Vector3Int> pieces = new Dictionary<PlayableCardInfo, Vector3Int>();
        int maxLength = Mathf.Min(startPieces.Count, startLocations.Count);
        for (int i = 0; i < maxLength; i++)
        {
            pieces[startPieces[i]] = startLocations[i];
        }
        return pieces;
    }
}
