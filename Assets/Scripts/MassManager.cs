using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassManager : MonoBehaviour
{
    #region Singleton
        public static MassManager instance;

        void Awake() 
        {
            if(instance != null)
            {
                Debug.LogWarning("More than one instance of MassManager found.");
                return;
            }
            instance = this;
        }
    #endregion

    [SerializeField]
    private GameObject worldGravity;

    private List<Transform> massTransforms;

    void Start()
    {
        massTransforms = new List<Transform>();
    }

    public List<Transform> MassTransforms
    {
        get{ return massTransforms; }
    }

    public GameObject WorldGravity
    {
        get{ return worldGravity; }
    }

    public bool HasMassTransforms()
    {
        return massTransforms.Count > 0;
    }

    public void AddMassTransform(Transform mass)
    {
        massTransforms.Add(mass);
    }

    public void RemoveMassTransform(Transform mass)
    {
        massTransforms.Remove(mass);
    }
}