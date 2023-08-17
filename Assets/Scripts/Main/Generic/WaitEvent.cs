using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitEvent : MonoBehaviour
{
    private float waitDuration;
    private Action action;


    // Instantiate
    public static WaitEvent CreateWaitEvent(float waitDuration, Action action)
    {
        WaitEvent waitEvent = Instantiate(Resources.Load<WaitEvent>("Prefabs/Generic/Wait Event"));
        waitEvent.waitDuration = waitDuration;
        waitEvent.action = action;
        return waitEvent;
    }

    
    // Wait for set duration
    public void StartWait()
    {
        StartCoroutine(Wait());
    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitDuration);
        action.Invoke();
        Destroy(this);
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
