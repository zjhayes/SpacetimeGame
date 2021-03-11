using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Power))]
[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Clock))]
public class TimerButtonController : MonoBehaviour
{
    [SerializeField]
    float duration = 5.0f;
    float currentTime = 0.0f;

    void Start()
    {
        GetComponent<Interactable>().onInteract += OnButtonPress;
    }

    void Update()
    {
        if(currentTime > 0.0f)
        {
            currentTime -= GetComponent<Clock>().TimeRelativeToPlayer;
            GetComponent<Power>().PowerOn();
        }
        else
        {
            GetComponent<Power>().PowerOff();
        }
    }

    void OnButtonPress()
    {
        currentTime = duration;
    }
}
