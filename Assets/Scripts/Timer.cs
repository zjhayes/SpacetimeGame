using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Power))]
[RequireComponent(typeof(Clock))]
public class Timer : MonoBehaviour
{
    [SerializeField]
    float duration = 5.0f;
    float currentTime = 0.0f;

    bool expired = true;

    void Start()
    {
        GetComponent<Power>().onPowerOn += Reset;
    }
    void Update()
    {
        if(currentTime > 0.0f)
        {
            currentTime -= GetComponent<Clock>().TimeRelativeToPlayer * Time.deltaTime;
        }
        else if(!expired)
        {
            GetComponent<Power>().PowerOff();
            expired = true;
        }
    }

    void Reset()
    {
        currentTime = duration; //* (1 / GetComponent<Clock>().TimeRelativeToPlayer);
        expired = false;
    }
}
