using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Clock))]
public class StairController : MonoBehaviour
{
    [SerializeField]
    List<GameObject> steps;
    [SerializeField]
    GameObject disabledPosition;
    [SerializeField]
    GameObject enabledPosition;
    [SerializeField]
    float speed = 5.0f;
    [SerializeField]
    bool enabled = false;
    [SerializeField]
    GameObject powerSource;
    [SerializeField]
    bool synchronize = false;
    [SerializeField]
    bool inverse = false;
    [SerializeField]
    bool reversePower = false;

    float distance = 0f;

    void Start()
    {
        Debug.Log(gameObject.name + " is using a deprecated script.");
    }

    void Awake()
    {
        distance = Vector3.Distance(enabledPosition.transform.position, disabledPosition.transform.position);
        powerSource.GetComponent<Power>().onPowerOn += Enable;
        powerSource.GetComponent<Power>().onPowerOff += Disable;
    }

    void Enable()
    {
        enabled = (!reversePower) ? true : false;
    }

    void Disable()
    {
        enabled = (!reversePower) ? false : true;
    }

    void Update()
    {
        distance = Vector3.Distance(enabledPosition.transform.position, disabledPosition.transform.position);
        
        if(enabled)
        {
            if(synchronize)
            {
                UpdateStepsSynchronized(enabledPosition.transform.position.y);
            }
            else
            {
                UpdateStepsScattered(enabledPosition.transform.position.y);
            }
        }
        else
        {
            UpdateStepsSynchronized(disabledPosition.transform.position.y);
        }
    }

    void UpdateStepsSynchronized(float height)
    {
        float changeAmount = speed * GetComponent<Clock>().TimeRelativeToPlayer() * Time.deltaTime;
        int index = 0;
        foreach(GameObject step in steps)
        {
            Vector3 target = new Vector3(step.transform.position.x, height, step.transform.position.z);
            step.transform.position = Vector3.MoveTowards(step.transform.position, target, changeAmount);
            index++;
        }
    }

    void UpdateStepsScattered(float height)
    {
        float changeAmount = speed * GetComponent<Clock>().TimeRelativeToPlayer() * Time.deltaTime;
        int index = 0;
        foreach(GameObject step in steps)
        {
            // Set target to only update Y position.
            height = disabledPosition.transform.position.y + CalculateHeight(index);
            Vector3 target = new Vector3(step.transform.position.x, height * getHeightMultiplier(), step.transform.position.z);
            step.transform.position = Vector3.MoveTowards(step.transform.position, target, changeAmount);
            index++;
        }
    }

    float getHeightMultiplier()
    {
        return (inverse) ? -1 : 1;
    }

    float CalculateHeight(float index)
    {
        return distance * ((index + 1) / steps.Count);
    }
}
