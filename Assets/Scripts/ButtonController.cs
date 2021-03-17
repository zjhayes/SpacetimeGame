using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Power))]
public class ButtonController : MonoBehaviour
{
    int weights = 0;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Weighted>())
        {
            weights++;
            GetComponent<Power>().PowerOn();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.GetComponent<Weighted>())
        {
            weights--;

            if(weights <= 0)
            {
                GetComponent<Power>().PowerOff();
            }
        }
    }

}
