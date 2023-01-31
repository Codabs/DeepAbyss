using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public Door door;

    public DetectionSensor sensor;
    public GameObject LG;
    public bool isOn = false;

 
    public void On()
    {
        LG.GetComponent<Light>().enabled = true;
        isOn = true;
    }

    public void Off()
    {
        LG.GetComponent<Light>().enabled = false;
        isOn = false;
    }
}
