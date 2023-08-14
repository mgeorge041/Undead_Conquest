using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fillbar : MonoBehaviour
{
    public float fill => fillMask.fillAmount;
    public Image fillMask;

    
    // Instantiate
    public static Fillbar CreateFillbar()
    {
        Fillbar fillbar = Instantiate(Resources.Load<Fillbar>("Prefabs/Player/Player UI/Fillbar"));
        return fillbar;
    }


    // Set fill mask
    public void SetFill(float fill)
    {
        fillMask.fillAmount = fill;
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
