using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    [SerializeField]
    Transform startPoint;
    [SerializeField]
    Transform endPoint;
    [SerializeField]
    float laserWidth = .2f;
    LineRenderer laserLine;

    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        laserLine.SetWidth(laserWidth, laserWidth);
    }

    void Update()
    {
        laserLine.SetPosition(0, startPoint.position);
        laserLine.SetPosition(1, endPoint.position);
    }
}
