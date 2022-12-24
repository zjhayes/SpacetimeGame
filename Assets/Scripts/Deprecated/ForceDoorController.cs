using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceDoorController : MonoBehaviour
{
    [SerializeField]
    GameObject powerSource;

    void Start()
    {
        Debug.Log(gameObject.name + " is using a deprecated script.");
    }

    void Awake()
    {
        powerSource.GetComponent<Power>().onPowerOn += Open;
        powerSource.GetComponent<Power>().onPowerOff += Close;
    }

    void Open()
    {
        gameObject.SetActive(false);
    }

    void Close()
    {
        gameObject.SetActive(true);
    }
}
