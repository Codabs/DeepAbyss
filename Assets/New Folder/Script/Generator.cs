using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public Door door;

    public GameObject _light;
    public bool isOn = false;

    Color color1 = Color.green;
    Color color2 = Color.red;

    private void Start()
    {
        _light.GetComponent<Light>().color = color2;
    }
    public void On()
    {
        _light.GetComponent<Light>().color = color1;
        isOn = true;
    }

    public void Off()
    {
        _light.GetComponent<Light>().color = color2;
        isOn = false;
    }
}
