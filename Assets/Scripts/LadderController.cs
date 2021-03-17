using UnityEngine;

public class LadderController : MonoBehaviour
{
    void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerMovement>().IsClimbing = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && PlayerManager.instance.Load.GetComponent<LoadManager>().HasLoad())
        {
            // Drop pickups.
            PlayerManager.instance.Load.GetComponent<LoadManager>().PutDown();
        }
    }

    void OnTriggerExit(Collider other) 
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerMovement>().IsClimbing = false;
        }
    }
}
