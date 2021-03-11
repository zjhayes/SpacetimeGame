using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    GameObject load;
    [SerializeField]
    GameObject laserPrefab;
    [SerializeField]
    float fireSpeed = 5.0f;

    void Start()
    {
        PlayerManager.instance.Player.GetComponent<PlayerInteraction>().onFire += Fire;
    }

    void Fire()
    {
        if(load.GetComponent<LoadManager>().HasLoad() || load.GetComponent<LoadManager>().RecentlyThrown()) { return; } // Player is carrying object.

        // Check if object being aimed at can be fired at.
        GameObject objectInView = PlayerManager.instance.Player.GetComponent<PlayerInteraction>().CurrentHit;
        if(objectInView.GetComponent<CenterOfMass>())
        {
            if(objectInView.GetComponent<CenterOfMass>().IsSet)
            {
                // This is the current center of mass, unset.
                objectInView.GetComponent<CenterOfMass>().Unset();
            }
            else
            {
                // Set object as center of mass.
                objectInView.GetComponent<CenterOfMass>().Set();
            }
        }
        else
        {
            ShootLaser();
        }
    }

    void ShootLaser()
    {
        GameObject newLaser = Instantiate(laserPrefab, this.transform.position, this.transform.rotation);
        newLaser.GetComponent<Laser>().StartPoint.GetComponent<Rigidbody>().velocity = this.transform.forward * fireSpeed;
        newLaser.GetComponent<Laser>().EndPoint.GetComponent<Rigidbody>().velocity = this.transform.forward * fireSpeed;
    }
}
