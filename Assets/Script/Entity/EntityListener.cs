using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityListener : MonoBehaviour
{
    //
    //VARIABLE
    //
    [SerializeField] private EntityBrain brain;
    [SerializeField] private float radius = 30f;
    public int listenSensibility = 5;
    private float olderTier = 0;
    private Vector3 winnerSound;
    public LayerMask soundMask;
    //
    //FONCTION
    //
    public Vector3 ListenForPlayerSound()
    {
        //Make a big Spherecast for sound like Sprint
        //Less the player see the entity, the more she can listen
        float radiusSphereCast = radius + brain.whenWasTheLastTimeTheEntityMeetThePlayer;
        RaycastHit[] listenRadius = Physics.SphereCastAll(transform.position, radiusSphereCast, Vector3.zero, soundMask);
        //Raycast hit to GameObject
        List<GameObject> objectHitByTheSphereCast = new();
        if (listenRadius.Length <= 0) return Vector3.zero;
        foreach(RaycastHit hit in listenRadius)
        {
            objectHitByTheSphereCast.Add(hit.collider.gameObject);
        }
        //Check If There Is a object 
        winnerSound = Vector3.zero;
        foreach(GameObject gameObj in objectHitByTheSphereCast)
        {
            if(gameObj.TryGetComponent<EntitySound>(out EntitySound sound))
            {
                if (sound.tier >= listenSensibility)
                {
                    if (olderTier <= sound.tier)
                    {
                        float olderTier = sound.tier;
                        winnerSound = sound.transform.position;
                    }
                }
            }
        }
        return winnerSound;
    }
    //
    //GIZMOS
    //
    private void OnDrawGizmos()
    {
        try 
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }
        catch { }
    }
}
