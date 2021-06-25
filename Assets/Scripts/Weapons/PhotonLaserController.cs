using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonLaserController : MonoBehaviour, IWeapon
{
    [SerializeField]
    GameObject laserPrefab;
    [SerializeField]
    float maxCharge = 3.0f;
    [SerializeField]
    float fireSpeed = 5.0f;
    [SerializeField]
    float cooldown = 1.0f;

    bool charging = false;
    float charge;
    float baseCharge = 1.0f;
    float currentCooldown = 0.0f;

    void Start()
    {
        charge = baseCharge;
    }

    void Update()
    {
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

    public void OnFireStarted()
    {
        if(currentCooldown < 0)
        {
            charging = true;
        }
    }

    public void Fire()
    {
        if(charging)
        {
            ShootLaser();
            charging = false;
            currentCooldown = cooldown;
        }
        charge = baseCharge;
    }

    public void PutAway()
    {
        Destroy(this.transform.gameObject);
    }

    void ShootLaser()
    {
        GameObject newLaser = Instantiate(laserPrefab, this.transform.forward + this.transform.position, this.transform.rotation);
        newLaser.GetComponent<Laser>().StartPoint.GetComponent<Rigidbody>().velocity = this.transform.forward * fireSpeed;
        newLaser.GetComponent<Laser>().EndPoint.GetComponent<Rigidbody>().velocity = this.transform.forward * fireSpeed;

        // Decrease the degrees of gravitational effect based on weapon charge.
        newLaser.GetComponent<Laser>().StartPoint.GetComponent<Mass>().Degrees *= 1/charge;
        newLaser.GetComponent<Laser>().EndPoint.GetComponent<Mass>().Degrees *= 1/charge;
    }
}
