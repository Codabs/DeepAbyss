using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonstre : MonoBehaviour
{
    public Color color = Color.red;
    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawCube(transform.position, Vector3.one * 0.1f);
    }
}
