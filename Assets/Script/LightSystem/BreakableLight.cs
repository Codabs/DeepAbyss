using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableLight : MonoBehaviour
{
    //
    //VARIABLE
    //
    public Light lightComponent;
    [SerializeField] private Material blackLightMaterial;
    [SerializeField] private Material whiteLightMaterial;


    //
    //FONCTION
    //
    public void ShutDownTheLight()
    {
        Destroy(gameObject);
    }
}
