using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCannonController : MonoBehaviour, IWeapon
{
    PlayerInteraction interact;

    void Start()
    {
        interact = PlayerManager.instance.Player.GetComponent<PlayerInteraction>();
    }

    public void OnFireStarted()
    {

    }

    public void Fire()
    {
        // Add or remove Mass from object.
        GameObject objectInView = interact.CurrentObject;
        if(objectInView.GetComponent<Massable>())
        {
            if(!objectInView.GetComponent<Massable>().HasMass)
            {
                objectInView.GetComponent<Massable>().Set();
            }
            else
            {
                objectInView.GetComponent<Massable>().Unset();
            }
        }
    }

    public void PutAway()
    {
        Destroy(this.transform.gameObject);
    }
}
