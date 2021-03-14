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
    private GameObject centerOfMassPrefab;

    private List<Transform> massTransforms;
    private GameObject centerOfMass;

    void Start()
    {
        massTransforms = new List<Transform>();
        centerOfMass = Instantiate(centerOfMassPrefab, this.transform.position, Quaternion.identity);
        SetMassActive();
    }

    void Update()
    {
        if(centerOfMass.GetComponent<CenterOfMass>().IsActive())
        {
            centerOfMass.transform.position = CalculateCenterOfMass();
        }
    }

    public void AddMassTransform(Transform mass)
    {
        massTransforms.Add(mass);
        SetMassActive();
    }

    public void RemoveMassTransform(Transform mass)
    {
        massTransforms.Remove(mass);
        SetMassActive();
    }

    private void SetMassActive()
    {
        // If any massable objects, set center of mass as active.
        centerOfMass.SetActive(massTransforms.Count > 0);
    }

    private Vector3 CalculateCenterOfMass()
    {
        Vector3 centroid = new Vector3();
        float massTotal = 0.0f;
        // Find center position of all mass objects, weighted by mass.
        foreach(Transform massObject in massTransforms)
        {
            float mass = massObject.gameObject.GetComponent<Weighted>().Weight;
            
            centroid += massObject.transform.position * mass;
            massTotal += mass;
        }
        centroid /= massTotal;
        
        return centroid;
    }

    public GameObject CenterOfMass
    {
        get { return centerOfMass; }
        set { centerOfMass = value; }
    }
}
