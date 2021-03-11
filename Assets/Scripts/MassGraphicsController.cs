using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassGraphicsController : MonoBehaviour
{
    [SerializeField]
    Material defaultMat;
    [SerializeField]
    Material chargedMat;

    Material currentMat;

    void Start()
    {
        UpdateMat();
        this.gameObject.transform.parent.GetComponent<CenterOfMass>().onMassChanged += UpdateMat;
    }

    void UpdateMat()
    {
        // If current object has mass, set as charged.
        if(this.gameObject.transform.parent.GetComponent<CenterOfMass>().IsSet)
        {
            currentMat = chargedMat;
        }
        else
        {
            currentMat = defaultMat;
        }
        UpdateGraphics();
    }

    void UpdateGraphics()
    {
        Material[] newMaterials = new Material[2];
        newMaterials[0] = this.GetComponent<Renderer>().materials[0];
        newMaterials[1] = currentMat;
        this.GetComponent<Renderer>().materials = newMaterials;
    }
}
