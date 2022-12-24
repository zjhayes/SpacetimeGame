using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    [SerializeField]
    string tooltipText;

        public string TooltipText
    {
        get{ return tooltipText; }
        set{ tooltipText = value; }
    }
}
