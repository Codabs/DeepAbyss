using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityEmeterWhenSprinting : MonoBehaviour
{
    [SerializeField] private new Rigidbody rigidbody;
    private void Update()
    {
        try
        {
            if(rigidbody.velocity.x + rigidbody.velocity.z >= 0.1f)
            {

            }
        }
        catch
        {

        }
    }
}
