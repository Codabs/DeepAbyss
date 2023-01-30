using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityEyes : MonoBehaviour
{
    //
    //VARIABLE
    //
    [Header("Eyes Variables/Objects")]
    public float spotAngleLong = 80f;
    public float viewDistanceLong;
    public float spotAngleShort = 180f;
    public float viewDistanceShort;
    public LayerMask viewMask;

    //
    //MONOBEHAVIOUR
    //

    //
    //FONCTION
    //

    public bool CanISeeThisTransform(Transform objectTransform)
    {
        bool value = false;
        Vector3 objectPosition = objectTransform.position;

        //We calculed check to time if the entity can see the location

        //One for a far but restricted angle
        bool farCheck = CanASeeThisLocation(viewDistanceLong, spotAngleLong, objectPosition);
        //One for a short but open angle so the entity can see if a player is on their left or right
        bool shortCheck = CanASeeThisLocation(viewDistanceShort, spotAngleShort, objectPosition);

        if(farCheck || shortCheck)
        {
            value = true;
        }
        return value;
    }
    private bool CanASeeThisLocation(float distance,float angle, Vector3 positionOfTheOtherObject)
    {
        if (Vector3.Distance(transform.position, positionOfTheOtherObject) < distance)
        {
            Vector3 dirToPlayer = (positionOfTheOtherObject - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, dirToPlayer);
            if (angleBetweenGuardAndPlayer < angle / 2f)
            {
                if (!Physics.Linecast(transform.position, positionOfTheOtherObject, viewMask))
                {
                    return true;
                }
            }
        }
        return false;
    }
    //
    //GIZMOS
    //
    private void OnDrawGizmos()
    {
        MakeGizmosLongFieldOfView();
        MakeGizmosShortFieldOfView();
    }
    private void MakeGizmosLongFieldOfView()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistanceLong);

        Quaternion leftRayRotationLong = Quaternion.AngleAxis(-(spotAngleLong / 2.0f), Vector3.up);
        Quaternion rightRayRotationLong = Quaternion.AngleAxis((spotAngleLong / 2.0f), Vector3.up);
        Vector3 leftRayDirectionLong = leftRayRotationLong * transform.forward;
        Vector3 rightRayDirectionLong = rightRayRotationLong * transform.forward;
        Gizmos.DrawRay(transform.position, leftRayDirectionLong * viewDistanceLong);
        Gizmos.DrawRay(transform.position, rightRayDirectionLong * viewDistanceLong);
    }
    private void MakeGizmosShortFieldOfView()
    {
        Gizmos.color = Color.blue;

        Quaternion leftRayRotationShort = Quaternion.AngleAxis(-(spotAngleShort / 2.0f), Vector3.up);
        Quaternion rightRayRotationShort = Quaternion.AngleAxis((spotAngleShort / 2.0f), Vector3.up);
        Vector3 leftRayDirectionShort = leftRayRotationShort * transform.forward;
        Vector3 rightRayDirectionShort = rightRayRotationShort * transform.forward;
        Gizmos.DrawRay(transform.position, leftRayDirectionShort * viewDistanceShort);
        Gizmos.DrawRay(transform.position, rightRayDirectionShort * viewDistanceShort);
    }
}
