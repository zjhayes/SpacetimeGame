using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Power))]
public class PhotonSensor : MonoBehaviour
{
    [SerializeField]
    float sensitivity = 1.0f;
    [SerializeField]
    bool continuous = false;

    float currentPower = 0.0f;

    void Update()
    {
        currentPower -= Time.deltaTime;
        
        if(!continuous && currentPower <= 0)
        {
            GetComponent<Power>().PowerOff();
        }
    }

    public void Power()
    {
        currentPower = sensitivity;
        GetComponent<Power>().PowerOn();
    }
}
