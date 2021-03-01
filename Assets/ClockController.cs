using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Clock))]
public class ClockController : MonoBehaviour
{
    private Clock clock;

    void Start()
    {
        clock = GetComponent<Clock>();
    }

    void Update()
    {
        Quaternion currentRotation = transform.rotation;
        float timeRelativeToPlayer = clock.TimeRelativeToPlayer;
        Debug.Log(timeRelativeToPlayer);
        transform.rotation = currentRotation * Quaternion.Euler(0, timeRelativeToPlayer, 0);
    }
}
