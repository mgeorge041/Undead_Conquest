using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class PixelButton : Button
{
    private Vector3 labelOffset = new Vector3(0, 1);
    public TextMeshProUGUI buttonLabel;


    // Offset text labels on button press
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        buttonLabel.transform.position -= labelOffset;
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        buttonLabel.transform.position += labelOffset;
    }
}
