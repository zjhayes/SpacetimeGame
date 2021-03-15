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
    [SerializeField]
    private float reach = 2.0f;
    private float playerClock = 60;

    public float DistanceFrom(GameObject other)
    {
        return Vector3.Distance(player.transform.position, other.transform.position);
    }

    public GameObject Player
    {
        get { return player; }
    }

    public float Reach
    {
        get { return reach; }
    }

    public float PlayerClock
    {
        get
        {
            return playerClock;// * Time.deltaTime;
        }
    }
}
