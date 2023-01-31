using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    //
    //VARIABLE
    //
    [SerializeField] private BreakableLight[] lightsInTheRoom = new BreakableLight[] { };
    public bool AreTheLightOn = true;
    [SerializeField] private Color gizmosColor = Color.white;
    //
    //MONOBEHIAVORE
    //
    private void Reset()
    {
        lightsInTheRoom = gameObject.GetComponentsInChildren<BreakableLight>();
    }
    //
    //Fonction
    //
    public IEnumerator ShutDownAllLightInTheRoom()
    {
        for(int i = 0; i <= lightsInTheRoom.Length; i++)
        {
            ShutDownThisLight(lightsInTheRoom[i]);
        }
        yield return new WaitForSeconds(3f);
        AreTheLightOn = false;
    }
    private void ShutDownThisLight(BreakableLight light)
    {
        StartCoroutine(light.ShutDownTheLight());
    }
    //
    //Gizmos
    //
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        for (int i = 0; i <= lightsInTheRoom.Length; i++)
        {
            Gizmos.DrawIcon(lightsInTheRoom[i].transform.position, "F");
        }
    }
}
