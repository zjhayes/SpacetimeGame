using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Mass : MonoBehaviour
{
    [SerializeField]
    float gravityRadius = 4.0f;
    [SerializeField]
    float degrees = 360;

    void Update()
    {
        GameObject centerOfMass = MassManager.instance.CenterOfMass;
        if(centerOfMass == null) { return; }

        float distance = Vector3.Distance(centerOfMass.transform.position, gameObject.transform.position);

        // Disable gravity if within gravityRadius.
        if(distance <= gravityRadius)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
        else if(!gameObject.GetComponent<Pickup>() || !gameObject.GetComponent<Pickup>().IsHolding) // enable gravity if user isn't holding object, or not pickup.
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
        
        float rotationSpeedDegrees = (1 / distance) * degrees; // degrees per second.
        Vector3 desiredDirection = (centerOfMass.transform.position + centerOfMass.GetComponent<CenterOfMass>().Offset - gameObject.transform.position).normalized;
        Vector3 newVelocity = Vector3.RotateTowards(gameObject.GetComponent<Rigidbody>().velocity, desiredDirection,
            rotationSpeedDegrees * Time.deltaTime * Mathf.Deg2Rad, 0);

        gameObject.GetComponent<Rigidbody>().velocity = newVelocity;
    }
}
