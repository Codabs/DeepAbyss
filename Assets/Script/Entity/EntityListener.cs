using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityListener : MonoBehaviour
{
    //
    //VARIABLE
    //
    [SerializeField] private float radius = 30f;
    [SerializeField] private float ListenRateInSecond = 0.2f;
    //
    //FONCTION
    //
    private void Start()
    {
        StartCoroutine(ListenForPlayerSound());
    }
    public IEnumerator ListenForPlayerSound()
    {
        //Make a big Spherecast for sound like Sprint

        //Check If There Is a object 

        //Wait Before Restart
        yield return new WaitForSeconds(ListenRateInSecond);
        StartCoroutine(ListenForPlayerSound());
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
