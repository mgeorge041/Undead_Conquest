using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public static class HexActionCalculator
{
    // Get move hexes
    public static List<Hex> GetMoveHexes(Unit unit)
    {
        if (unit == null || unit.hex == null)
            throw new System.ArgumentNullException("Cannot get move hexes for null unit or hex.");

        List<Hex> moveHexes = HexPathfinding.GetMaxPath(unit.hex, unit.unitData.currentSpeed);

        return moveHexes;
    }


    // Get all hexes a piece can potentially attack 
    public static List<Hex> GetAttackableHexes(Piece piece)
    {
        if (piece == null || piece.hex == null)
            throw new System.ArgumentNullException("Cannot get attackable hexes for null piece or hex.");

        List<Hex> moveHexes;
        List<Hex> attackableHexes = new List<Hex>();
        
        // Get move hexes
        if (piece.isUnit)
        {
            moveHexes = GetMoveHexes((Unit)piece);
        }
        else
        {
            moveHexes = new List<Hex>() { piece.hex };
        }

        // Get attackable hexes in range of move hexes
        foreach (Hex moveHex in moveHexes)
        {
            List<Hex> hexesInRange = HexPathPattern.GetHexesInRange(moveHex, piece.pieceData.range);
            foreach (Hex hex in hexesInRange) 
            {
                if (!attackableHexes.Contains(hex))
                    attackableHexes.Add(hex);
            }
        }

        attackableHexes.Remove(piece.hex);
        return attackableHexes;
    }


    // Get all hexes with enemy pieces
    public static List<Hex> GetAttackHexes(Piece piece)
    {
        if (piece == null || piece.hex == null)
            throw new System.ArgumentNullException("Cannot get attack hexes for null piece or hex.");

        List<Hex> attackHexes = new List<Hex>();
        List<Hex> attackableHexes = GetAttackableHexes(piece);
        foreach (Hex attackableHex in attackableHexes)
        {
            if (attackableHex.hasPiece && !attackableHex.piece.IsFriendly(piece))
                attackHexes.Add(attackableHex);
        }

        return attackHexes;
    }


    // Get all hexes within attack range of target hex
    public static List<Hex> GetAttackRangeHexes(Hex targetHex, int pieceRange)
    {
        if (pieceRange <= 0 || targetHex == null || !targetHex.hasPiece)
            throw new System.ArgumentNullException("Cannot get attack range hexes for 0 range or null hex or piece.");

        List<Hex> attackRangeHexes = HexPathPattern.GetHexesInRange(targetHex, pieceRange, false);
        return attackRangeHexes;
    }
}
