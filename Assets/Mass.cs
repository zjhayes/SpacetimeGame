﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Mass : MonoBehaviour
{
    List<GameObject> moons;

    void Start()
    {
        moons = new List<GameObject>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Pickup>())
        {
            moons.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(moons.Contains(other.gameObject))
        {
            other.GetComponent<Rigidbody>().useGravity = true;
            moons.Remove(other.gameObject);
        }
    }

    void Update()
    {
        foreach(GameObject moon in moons)
        {
            moon.GetComponent<Rigidbody>().useGravity = false;
            float distance = Vector3.Distance(this.transform.position, moon.transform.position);

            float rotationSpeedDegrees = (1 / distance) * 360; // 180 degrees per second.
            Vector3 desiredDirection = (this.transform.position - moon.transform.position).normalized;
            Vector3 newVelocity = Vector3.RotateTowards(moon.GetComponent<Rigidbody>().velocity, desiredDirection,
                rotationSpeedDegrees * Time.deltaTime * Mathf.Deg2Rad, 0 );

            moon.GetComponent<Rigidbody>().velocity = newVelocity;
        }
    }
}
