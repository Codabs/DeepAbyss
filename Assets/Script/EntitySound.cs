using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySound : MonoBehaviour
{
    public float tier = 0;
    private void Awake()
    {
        StartCoroutine(AutoDestroy());
    }

    private IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(0.1f);
    }
}
