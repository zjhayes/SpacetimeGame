using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
        public static PlayerManager instance;

        void Awake() 
        {
            if(instance != null)
            {
                Debug.LogWarning("More than one instance of PlayerManager found.");
                return;
            }
            instance = this;
        }
    #endregion

    [SerializeField]
    private GameObject player;
    private float playerClock = 60;

    public float DistanceFrom(GameObject other)
    {
        return Vector3.Distance(player.transform.position, other.transform.position);
    }

    public float PlayerClock
    {
        get
        {
            return playerClock;// * Time.deltaTime;
        }
    }
}
