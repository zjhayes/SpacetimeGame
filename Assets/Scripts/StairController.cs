using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StairController : MonoBehaviour
{
    [SerializeField]
    List<GameObject> stairs;
    [SerializeField]
    GameObject disabledPosition;
    [SerializeField]
    GameObject enabledPosition;
    [SerializeField]
    float speed = 1.0f;
    [SerializeField]
    bool enabled = false;
    [SerializeField]
    GameObject powerSource;

    float distance = 0f;

    void Awake()
    {
        distance = Vector3.Distance(enabledPosition.transform.position, disabledPosition.transform.position);
        powerSource.GetComponent<Power>().onPowerOn += Enable;
        powerSource.GetComponent<Power>().onPowerOff += Disable;
    }

    void Enable()
    {
        enabled = true;
    }

    void Disable()
    {
        enabled = false;
    }

    void Update()
    {
        if(enabled)
        {
            UpdateEnable();
        }
        else
        {
            UpdateDisable();
        }
    }

    void UpdateEnable()
    {
        float step = speed * Time.deltaTime;
        int index = 0;
        foreach(GameObject stair in stairs)
        {
            // Set target to only update Y position.
            float height = disabledPosition.transform.position.y + CalculateHeight(index);
            Vector3 target = new Vector3(stair.transform.position.x, height, stair.transform.position.z);
            stair.transform.position = Vector3.MoveTowards(stair.transform.position, target, step);
            index++;
        }
    }

    void UpdateDisable()
    {
        float step = speed * Time.deltaTime;
        int index = 0;
        foreach(GameObject stair in stairs)
        {
            // Set target to only update Y position.
            float height = disabledPosition.transform.position.y;
            Vector3 target = new Vector3(stair.transform.position.x, height, stair.transform.position.z);
            stair.transform.position = Vector3.MoveTowards(stair.transform.position, target, step);
            index++;
        }
    }

    float CalculateHeight(float index)
    {
        return distance * ((index + 1) / stairs.Count);
    }
}
