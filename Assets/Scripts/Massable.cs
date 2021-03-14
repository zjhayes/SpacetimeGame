using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Massable : MonoBehaviour
{
    [SerializeField]
    bool hasMass = false;

    public delegate void OnMassChanged();
    public OnMassChanged onMassChanged;

    void Start()
    {
        if(hasMass)
        {
            // Has mass by default.
            Set();
        }
    }

    public void Set()
    {
        MassManager.instance.AddMassTransform(this.transform);
        hasMass = true;
        InvokeOnMassChanged();
    }

    public void Unset()
    {
        MassManager.instance.RemoveMassTransform(this.transform);
        hasMass = false;
        InvokeOnMassChanged();
    }

    public bool HasMass
    {
        get{ return hasMass; }
        set{ hasMass = value; }
    }

    private void InvokeOnMassChanged()
    {
        if(onMassChanged != null)
        {
            onMassChanged.Invoke();
        }
    }
}
