using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceCountContainer : MonoBehaviour
{
    // Resource info
    public Image resourceIcon;
    public TextMeshProUGUI resourceAmount;

    // Event manager
    public ResourceCountContainerEventManager eventManager { get; private set; } = new ResourceCountContainerEventManager();

    // Instantiate
    public static ResourceCountContainer CreateResourceCountContainer()
    {
        ResourceCountContainer container = Instantiate(Resources.Load<ResourceCountContainer>("Prefabs/Player/Player UI/Resource Count Container"));
        container.Initialize();
        return container;
    }


    // Initialize
    public void Initialize() { }


    // Reset 
    public void Reset()
    {
        resourceIcon.sprite = null;
        resourceAmount.text = 0.ToString();
    }


    // Set resource information
    public void SetResourceInformation(ResourceType resource, int amount)
    {
        SetResourceInformation(resource, amount.ToString());
    }
    public void SetResourceInformation(ResourceType resource, string text)
    {
        if (resource != ResourceType.None)
            resourceIcon.sprite = ResourceCard.LoadResourceSprite(resource);
        else
            resourceIcon.sprite = null;
        resourceAmount.text = text;
    }


    // Show rising above piece when gaining resource
    public void ShowGainingResource(Vector3 startPosition)
    {
        gameObject.SetActive(true);
        StartCoroutine(AnimateGainingResource(startPosition));
    }


    // Animate gaining resource
    private IEnumerator AnimateGainingResource(Vector3 startPosition)
    {
        float t = 0;
        transform.position = startPosition;
        Vector3 endPosition = startPosition + new Vector3(0, 0.5f);
        while (t < 1)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            t += Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
        eventManager.onHide.OnEvent(this);
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
