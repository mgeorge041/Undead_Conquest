using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TargetPatternClass
{
    public static Sprite lineTargetIconSprite = GetTargetIcon("Target Icon 5px Line");
    public static Sprite diagonalTargetIconSprite = GetTargetIcon("Target Icon 5px Diagonal");
    public static Sprite rangeTargetIconSprite = GetTargetIcon("Target Icon 5px Range");


    private static Sprite GetTargetIcon(string targetIconName)
    {
        return Resources.Load<Sprite>("Art/UI/" + targetIconName);
    }

    /*
    public static Sprite GetTargetIcon(EffectPattern pattern)
    {
        switch (pattern)
        {
            case EffectPattern.Area:
                return rangeTargetIconSprite;
            case EffectPattern.Fan:
                return lineTargetIconSprite;
            case EffectPattern.Line:
                return lineTargetIconSprite;
            case EffectPattern.Ring:
                return lineTargetIconSprite;
            case EffectPattern.SpreadV:
                return lineTargetIconSprite;
            case EffectPattern.TShape:
                return lineTargetIconSprite;
            case EffectPattern.VShape:
                return diagonalTargetIconSprite;
            case EffectPattern.Wall:
                return diagonalTargetIconSprite;
            default:
                return rangeTargetIconSprite;
        }
    }
    */
}
