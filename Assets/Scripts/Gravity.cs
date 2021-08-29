using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField]
    float mass;

    void FixedUpdate() 
    {
        foreach(Transform massTransform in MassManager.instance.MassTransforms)
        {
            ApplyGravity(massTransform);
        }
        ApplyGravity(MassManager.instance.WorldGravity.transform);
    }

    void ApplyGravity(Transform massTransform)
    {
        Vector3 direction = massTransform.position - transform.position;
        float gForce = mass / direction.sqrMagnitude;
        GetComponent<Rigidbody>().AddForce(direction.normalized * gForce * Time.deltaTime);
    }
}
