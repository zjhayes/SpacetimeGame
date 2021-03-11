using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Power))]
public class ButtonController : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Pickup>())
        {
            GetComponent<Power>().PowerOn();
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.GetComponent<Pickup>())
        {
            GetComponent<Power>().PowerOff();
        }
    }

}
