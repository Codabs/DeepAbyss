using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityTriggerGameOver : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            DeathCanvas.Instance.Death();
        }
    }
}
