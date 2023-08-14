using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

public class ResourcePanel : MonoBehaviour
{
    // Initialization
    public bool initialized { get; private set; }

    // Resources
    public Dictionary<ResourceType, Image> resourceIcons = new Dictionary<ResourceType, Image>();
    public Dictionary<ResourceType, TextMeshProUGUI> resourceLabels = new Dictionary<ResourceType, TextMeshProUGUI>();
    public Image boneIcon;
    public Image corpseIcon;
    public Image manaIcon;
    public Image stoneIcon;
    public Image woodIcon;
    public TextMeshProUGUI boneLabel;
    public TextMeshProUGUI corpseLabel;
    public TextMeshProUGUI manaLabel;
    public TextMeshProUGUI stoneLabel;
    public TextMeshProUGUI woodLabel;


    // Instantiate resource panel
    public static ResourcePanel CreateResourcePanel()
    {
        ResourcePanel panel = Instantiate(Resources.Load<ResourcePanel>("Prefabs/Player/Player UI/Resource Panel"));
        panel.Initialize();
        return panel;
    }


    // Initialize
    public void Initialize()
    {
        resourceIcons[ResourceType.Bone] = boneIcon;
        resourceIcons[ResourceType.Corpse] = corpseIcon;
        resourceIcons[ResourceType.Mana] = manaIcon;
        resourceIcons[ResourceType.Stone] = stoneIcon;
        resourceIcons[ResourceType.Wood] = woodIcon;
        foreach (Image icon in resourceIcons.Values)
        {
            icon.material = Instantiate(AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/Outline Material.mat"));
        }

        resourceLabels[ResourceType.Bone] = boneLabel;
        resourceLabels[ResourceType.Corpse] = corpseLabel;
        resourceLabels[ResourceType.Mana] = manaLabel;
        resourceLabels[ResourceType.Stone] = stoneLabel;
        resourceLabels[ResourceType.Wood] = woodLabel;
        foreach (TextMeshProUGUI label in resourceLabels.Values)
        {
            label.fontMaterial = Instantiate(AssetDatabase.LoadAssetAtPath<Material>("Assets/Fonts/Test Font.asset"));
        }
        initialized = true;
    }


    // Reset
    public void Reset()
    {
        resourceLabels[ResourceType.Bone].text = 0.ToString();
        resourceLabels[ResourceType.Corpse].text = 0.ToString();
        resourceLabels[ResourceType.Mana].text = 0.ToString();
        resourceLabels[ResourceType.Stone].text = 0.ToString();
        resourceLabels[ResourceType.Wood].text = 0.ToString();
    }


    // Get or set resource label
    public int GetResource(ResourceType resource)
    {
        TextMeshProUGUI label;
        if (!resourceLabels.TryGetValue(resource, out label))
            throw new System.ArgumentException("Resource panel does not contain resource label of type: " + resource);

        string labelText = label.text;
        return int.Parse(labelText);
    }
    public void SetResource(ResourceType resource, int amount)
    {
        TextMeshProUGUI label;
        if (!resourceLabels.TryGetValue(resource, out label))
            throw new System.ArgumentException("Resource panel does not contain resource label of type: " + resource);

        int currentAmount = GetResource(resource);
        if (currentAmount == amount)
            return;
        else if (currentAmount < amount)
            ShowResourceHighlight(resource, Color.green);
        else
            ShowResourceHighlight(resource, Color.red);
        label.text = amount.ToString();
    }


    // Show resource highlight
    public void ShowResourceHighlight(ResourceType resource, Color highlightColor)
    {
        StartCoroutine(ShowHighlightDuration(resource, highlightColor));
    }


    // Highlight for given duration
    private IEnumerator ShowHighlightDuration(ResourceType resource, Color highlightColor)
    {
        float duration = Mathf.PI / 2;
        Material iconMaterial = resourceIcons[resource].material;
        Material labelMaterial = resourceLabels[resource].fontMaterial;
        HighlightMaterial(iconMaterial, -1, highlightColor);
        HighlightMaterial(labelMaterial, 1, highlightColor);
        yield return new WaitForSeconds(duration);
        ClearHighlightMaterial(iconMaterial);
        ClearHighlightMaterial(labelMaterial);
    }


    // Highlight material
    private void HighlightMaterial(Material material, int outside, Color highlightColor)
    {
        material.SetColor("Outline_Color", highlightColor);
        material.SetFloat("Outside", outside);
        material.SetFloat("Flash_Speed", 1);
        material.SetFloat("Current_Time", Time.time);

    }
    private void ClearHighlightMaterial(Material material)
    {
        material.SetFloat("Outside", 0);
        material.SetFloat("Flash_Speed", 0);
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
