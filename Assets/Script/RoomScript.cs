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
        if (AreTheLightOn) 
        {
            AreTheLightOn = false;
            for (int i = 0; i <= lightsInTheRoom.Length; i++)
            {
                lightsInTheRoom[i].ShutDownTheLight();
            }
            yield return new WaitForSeconds(1f);
            this.enabled = false;
        }
    }
    //
    //Gizmos
    //
    private void OnDrawGizmos()
    {
        try
        {
            Gizmos.color = gizmosColor;
            for (int i = 0; i <= lightsInTheRoom.Length; i++)
            {
                Gizmos.DrawWireCube(lightsInTheRoom[i].transform.position, Vector3.one * 0.5f);
            }
        }
        catch { }
    }
}
