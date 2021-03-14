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
    [SerializeField]
    float cooldown = 1.0f;
    [SerializeField]
    float chargeMultiplier = 2.0f;
    [SerializeField]
    float maxCharge = 3.0f;

    PlayerInteraction interact;
    bool charging = false;
    float charge;
    float baseCharge = 1.0f;
    float currentCooldown = 0.0f;

    void Start()
    {
        interact = PlayerManager.instance.Player.GetComponent<PlayerInteraction>();
        InputManager.instance.Controls.Player.Fire.started += ctx => OnFireStarted();
        InputManager.instance.Controls.Player.Fire.canceled += ctx => Fire();
        InputManager.instance.Controls.Player.Special.performed += ctx => Special();
        charge = baseCharge;
    }

    void Update()
    {
        PointGun();

        if(charging)
        {
            charge += Time.deltaTime;
        }
        // Autofire if charge is at max.
        if(charge >= maxCharge)
        {
            Fire();
        }

        currentCooldown -= Time.deltaTime;
    }

    void PointGun()
    {
        if(interact.HasHit)
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
    }

    void OnFireStarted()
    {
        // Throw if carrying, otherwise charge weapon.
        if(load.GetComponent<LoadManager>().HasLoad()) 
        {
            load.GetComponent<LoadManager>().Throw();
        }
        else if(currentCooldown < 0)
        {
            charging = true;
        }
    }

    void Fire()
    {
        if(charging)
        {
            ShootLaser();
            charging = false;
            currentCooldown = cooldown;
        }
        charge = baseCharge;
    }

    void ShootLaser()
    {
        GameObject newLaser = Instantiate(laserPrefab, this.transform.forward + this.transform.position, this.transform.rotation);
        newLaser.GetComponent<Laser>().StartPoint.GetComponent<Rigidbody>().velocity = this.transform.forward * fireSpeed;
        newLaser.GetComponent<Laser>().EndPoint.GetComponent<Rigidbody>().velocity = this.transform.forward * fireSpeed;

        // Decrease the degrees of gravitational effect based on weapon charge.
        newLaser.GetComponent<Laser>().StartPoint.GetComponent<Mass>().Degrees *= 1/charge;
        newLaser.GetComponent<Laser>().EndPoint.GetComponent<Mass>().Degrees *= 1/charge;
        Debug.Log(newLaser.GetComponent<Laser>().EndPoint.GetComponent<Mass>().Degrees);
    }

    void Special()
    {
        // Check if object being aimed at is massable.
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
    }
}
