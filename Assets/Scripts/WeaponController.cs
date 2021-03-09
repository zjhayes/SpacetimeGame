using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    GameObject load;


    void Start()
    {
        PlayerManager.instance.Player.GetComponent<PlayerInteraction>().onFire += Fire;
    }

    void Fire()
    {
        // Check if object being aimed at can be picked up.
        GameObject objectInView = PlayerManager.instance.Player.GetComponent<PlayerInteraction>().CurrentHit;
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
}
