using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Mass : MonoBehaviour
{
    [SerializeField]
    float gravityRadius = 4.0f;
    GameObject centerOfMass;

    void Start()
    {
        centerOfMass = MassManager.instance.CenterOfMass;
    }

    void Update()
    {
        float distance = Vector3.Distance(centerOfMass.transform.position, gameObject.transform.position);

        // Disable gravity if within gravityRadius.
        if(distance <= gravityRadius)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
        else if(!gameObject.GetComponent<Pickup>().IsHolding) // enable gravity if user isn't holding object.
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
        
        float rotationSpeedDegrees = (1 / distance) * 360; // degrees per second.
        Vector3 desiredDirection = (centerOfMass.transform.position - gameObject.transform.position).normalized;
        Vector3 newVelocity = Vector3.RotateTowards(gameObject.GetComponent<Rigidbody>().velocity, desiredDirection,
            rotationSpeedDegrees * Time.deltaTime * Mathf.Deg2Rad, 0);

        gameObject.GetComponent<Rigidbody>().velocity = newVelocity;
    }
}
