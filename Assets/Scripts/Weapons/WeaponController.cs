using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    float pointSpeed = 1.0f;
    [SerializeField]
    float minRotation= 330.0f;
    [SerializeField]
    float maxRotation = 360.0f;
    [SerializeField]
    List<GameObject> weapons;
    int currentIndex = 0;
    IWeapon currentWeapon;
    PlayerInteraction interact;

    void Start()
    {
        interact = PlayerManager.instance.Player.GetComponent<PlayerInteraction>();
        InputManager.instance.Controls.Player.Fire.started += ctx => OnFireStarted();
        InputManager.instance.Controls.Player.Fire.canceled += ctx => Fire();
        InputManager.instance.Controls.Player.ChangeWeapon.performed += ctx => ChangeWeapon();


        // Set default weapon.
        if(weapons.Count != 0)
        {
            EquipWeapon(weapons[currentIndex++]);
        }
    }

    void Update()
    {
        PointGun();
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

    void EquipWeapon(GameObject weaponPrefab)
    {
        currentWeapon = InstantiateWeaponPrefab(weaponPrefab).GetComponent<IWeapon>();
    }

    GameObject InstantiateWeaponPrefab(GameObject weaponPrefab)
    {
        return Instantiate(weaponPrefab, this.transform.position, this.transform.rotation, this.transform);
    }

    void OnFireStarted()
    {
        currentWeapon.OnFireStarted();
    }

    void Fire()
    {
        currentWeapon.Fire();
    }

    void ChangeWeapon()
    {
        currentWeapon.PutAway();
        // Cycle through weapons.
        if(weapons.Count <= currentIndex)
        {
            currentIndex = 0;
        }
        EquipWeapon(weapons[currentIndex++]);
    }

    void Special()
    {
        // Check if object being aimed at is massable.
        GameObject objectInView = interact.CurrentObject;
        if(interact.HasHit && objectInView.GetComponent<Massable>())
        {
            if(objectInView.GetComponent<Massable>().HasMass)
            {
                // This is the current center of mass, unset.
                objectInView.GetComponent<Massable>().Unset();
            }
            else
            {
                // Set object as center of mass.
                objectInView.GetComponent<Massable>().Set();
            }
        }
    }
}
