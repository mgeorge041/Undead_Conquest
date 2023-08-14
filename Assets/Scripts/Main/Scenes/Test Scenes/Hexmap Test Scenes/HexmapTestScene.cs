using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexmapTestScene : MonoBehaviour
{
    public Hexmap hexmap;

    public void StartScene()
    {
        hexmap.Initialize();
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
