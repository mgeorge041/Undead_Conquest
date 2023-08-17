using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public static class OutlineText
{
    // Set font material
    public static void SetFontMaterial(TextMeshProUGUI label)
    {
        label.fontMaterial = Object.Instantiate(AssetDatabase.LoadAssetAtPath<Material>("Assets/Fonts/Test Font.asset"));
    }


    // Highlight for given duration
    public static IEnumerator ShowHighlightDuration(Material fontMaterial, float duration)
    {
        HighlightMaterial(fontMaterial, 1, Color.yellow);
        yield return new WaitForSeconds(duration);
        ClearHighlightMaterial(fontMaterial);
    }


    // Highlight material
    private static void HighlightMaterial(Material material, int outside, Color highlightColor)
    {
        material.SetColor("Outline_Color", highlightColor);
        material.SetFloat("Outside", outside);
        material.SetFloat("Flash_Speed", 1);
        material.SetFloat("Current_Time", Time.time);
    }
    private static void ClearHighlightMaterial(Material material)
    {
        material.SetFloat("Outside", 0);
        material.SetFloat("Flash_Speed", 0);
    }
}
