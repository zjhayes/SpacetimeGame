﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    float speed = 600.0f;

    void Start()
    {
        InputManager.instance.Controls.Player.Fire.performed += ctx => Fire();
        Debug.Log(gameObject.name + " is using a deprecated script.");
    }

    void Fire()
    {
        GameObject newBullet = Instantiate(bullet, (this.transform.position + transform.forward), Quaternion.identity);
        newBullet.GetComponent<Rigidbody>().velocity = this.transform.forward * speed;
    }
}