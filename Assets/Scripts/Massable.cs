using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Massable : MonoBehaviour
{
    [SerializeField]
    bool hasMass = false;

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
        // Set this as parent of CenterOfMass.
        MassManager.instance.CenterOfMass.SetActive(true);
        MassManager.instance.CenterOfMass.transform.position=this.transform.position;
        MassManager.instance.CenterOfMass.transform.parent=this.transform;
        hasMass = true;
    }

    public void Unset()
    {
        MassManager.instance.CenterOfMass.transform.parent=null;
        MassManager.instance.CenterOfMass.SetActive(false);
        hasMass = false;
    }

    public bool HasMass
    {
        get{ return hasMass; }
        set{ hasMass = value; }
    }
}
