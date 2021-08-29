using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weighted : MonoBehaviour
{
    [SerializeField]
    float weight = 15.0f;

    public float Weight
    {
        get{ return weight; }
    }
}
