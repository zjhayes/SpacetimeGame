using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour
{
    public delegate void OnPowerOn();
    public OnPowerOn onPowerOn;

    public delegate void OnPowerOff();
    public OnPowerOff onPowerOff;

    public void PowerOn()
    {
        if(onPowerOn != null)
        {
            onPowerOn.Invoke();
        }
    }

    public void PowerOff()
    {
        if(onPowerOff != null)
        {
            onPowerOff.Invoke();
        }
    }
}
