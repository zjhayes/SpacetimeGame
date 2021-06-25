using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftBuddyController : MonoBehaviour, IWeapon
{
    [SerializeField]
    GameObject load;

    public void OnFireStarted()
    {
        load.GetComponent<LoadManager>().OnPickUp();
    }

    public void Fire()
    {
        load.GetComponent<LoadManager>().PutDown();
    }

    public void PutAway()
    {
        if(load.GetComponent<LoadManager>().HasLoad())
        {
            load.GetComponent<LoadManager>().PutDown();
        }
        Destroy(this.transform.gameObject);
    }
}
