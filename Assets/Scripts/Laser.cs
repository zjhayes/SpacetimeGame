using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour {

    [SerializeField]
    Transform startPoint;
    [SerializeField]
    Transform endPoint;
    [SerializeField]
    float laserWidth = .2f;
    LineRenderer laserLine;
    float destroyDelay = 0.5f;

    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        laserLine.SetWidth(laserWidth, laserWidth);
    }

    void Update()
    {
        if(startPoint != null && endPoint != null)
        {
            laserLine.SetPosition(0, startPoint.position);
            laserLine.SetPosition(1, endPoint.position);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Transform StartPoint
    {
        get { return startPoint; }
    }

    public Transform EndPoint
    {
        get { return endPoint; }
    }
}
