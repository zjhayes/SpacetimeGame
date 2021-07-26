using UnityEngine;

public class LadderController : MonoBehaviour
{
    [SerializeField]
    float maxAngleOfContact = 10.0f;

    void Start()
    {
        Debug.Log(gameObject.name + " is using a deprecated script.");
    }


    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if(PlayerFacingLadder())
            {
                other.gameObject.GetComponent<PlayerMovement>().IsClimbing = true;

                // if(PlayerCarryingPickup())
                // {
                //     // Drop pickups.
                //     PlayerManager.instance.Load.GetComponent<LoadManager>().PutDown();
                // }
            }
            else
            {
                other.gameObject.GetComponent<PlayerMovement>().IsClimbing = false;
            }
        }
    }

    void OnTriggerExit(Collider other) 
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerMovement>().IsClimbing = false;
        }
    }

    bool PlayerFacingLadder()
    {
        // Returns true if player is facing ladder within a certain range.
        return PlayerManager.instance.Player.GetComponent<PlayerInteraction>().IsFacing(gameObject, maxAngleOfContact);
    }

    // bool PlayerCarryingPickup()
    // {
    //     // Returns true if player has pickup.
    //     return PlayerManager.instance.Load.GetComponent<LoadManager>().HasLoad();
    // }
}
