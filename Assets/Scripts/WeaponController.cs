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
    [SerializeField]
    float pointSpeed = 1.0f;
    [SerializeField]
    float minRotation= 330.0f;
    [SerializeField]
    float maxRotation = 360.0f;

    PlayerInteraction interact;

    void Start()
    {
        PlayerManager.instance.Player.GetComponent<PlayerInteraction>().onFire += Fire;
        interact = PlayerManager.instance.Player.GetComponent<PlayerInteraction>();
    }

    void Update()
    {
        PointGun();
    }

    void PointGun()
    {
        if(interact.HasHit)// && interact.DistanceToHit() > minDistance)
        {
            // Point gun towards object being aimed at.
            Vector3 aimPoint = interact.CurrentPoint;
            Vector3 targetDirection = aimPoint - transform.position;
            float speed = pointSpeed * Time.deltaTime;
            Vector3 pointDirection = Vector3.RotateTowards(transform.forward, targetDirection, speed, 0.0f);
            Debug.DrawRay(transform.position, pointDirection, Color.red);
            transform.rotation = Quaternion.LookRotation(pointDirection);
            float clampedY = Mathf.Clamp(transform.localEulerAngles.y, minRotation, maxRotation);
            Vector3 clampedRotation = new Vector3(transform.localEulerAngles.x, clampedY, transform.localEulerAngles.z);
            transform.localRotation = Quaternion.Euler(clampedRotation);
        }
        // Else transform looking forward? 
    }

    void Fire()
    {
        if(load.GetComponent<LoadManager>().HasLoad() || load.GetComponent<LoadManager>().RecentlyThrown()) { return; } // Player is carrying object.

        // Check if object being aimed at can be fired at.
        GameObject objectInView = interact.CurrentObject;
        if(interact.HasHit && objectInView.GetComponent<CenterOfMass>())
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
