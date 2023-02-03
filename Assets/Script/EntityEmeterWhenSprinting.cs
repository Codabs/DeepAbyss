using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityEmeterWhenSprinting : MonoBehaviour
{
    Vector3 lastPosition = Vector3.zero;
    public LayerMask soundLayer = 0;
    private void Update()
    {
        try
        {
            Vector3 velocity = transform.position - lastPosition;
            lastPosition = transform.position;
            if(velocity.x + velocity.z >= 0)
            {
                GameObject sound = new GameObject("sound");
                sound.layer = soundLayer;
                EntitySound scriptSound = sound.AddComponent<EntitySound>();
                scriptSound.tier = (velocity.x + velocity.z) * 100;
                print(scriptSound.tier);
            }
        }
        catch
        {

        }
    }
}
