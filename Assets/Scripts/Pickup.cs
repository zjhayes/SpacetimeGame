using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Pickup : MonoBehaviour
{
    [SerializeField]
    float loadCorrectionSpeed = 1.0f;

    bool isHolding = false;
    GameObject currentLoad;
    Vector3 objectPosition;

    void Update()
    {
        if(isHolding)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, currentLoad.transform.position, loadCorrectionSpeed);
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }

    public bool IsHolding
    {
        get{ return isHolding; }
    }

    public void PickUp(GameObject load)
    {
        currentLoad = load;
        this.transform.parent=currentLoad.transform;
        this.GetComponent<Rigidbody>().detectCollisions = true;

        isHolding = true;
    }

    public void PutDown()
    {
        objectPosition = this.transform.position;
        this.transform.SetParent(null);
        this.transform.position = objectPosition;
        currentLoad = null;

        isHolding = false;
    }

    public void Throw(GameObject load, float throwForce)
    {
        this.GetComponent<Rigidbody>().AddForce(load.transform.forward * throwForce);
        PutDown();
    }
}
