using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

[CustomEditor(typeof(PixelButton))]
public class PixelButtonEditor : UnityEditor.UI.ButtonEditor
{
    private PixelButton pixelButton;

    // Inspector updates
    public override void OnInspectorGUI()
    {
        pixelButton = (PixelButton)target;
        serializedObject.Update();
        pixelButton.buttonLabel = (TextMeshProUGUI)EditorGUILayout.ObjectField("Button label:", pixelButton.buttonLabel, typeof(TextMeshProUGUI), true);
        base.OnInspectorGUI();
        serializedObject.ApplyModifiedProperties();
    }
}
