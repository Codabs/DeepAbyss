using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EntityPathfing : MonoBehaviour
{
    //
    //VARIABLE
    //
    [Header("Patrol Variables/Objects")]
    public Transform[] pathHolder;
    public float patrolSpeed = 5;
    public float buffer = 0.05f;
    public float stopChance = 0.25f;

    [Header("Nav Variables")]
    private float maxSpeed = 7f;
    private float minSpeed = 1f;

    [Header("Component")]
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private EntityBrain brain;

    //Script variable
    private Vector3[] waypoints;
    private Vector3 startWaypoint;
    int targetWaypointIndex;
    private Vector3 targetWaypoint;

    //
    //FONCTION
    //
    public void ChosePath(int pathNumber)
    {
        //We check if the pathNumber is valid
        if (pathNumber > pathHolder.Length) return;

        //We get all the waypoints in the path
        waypoints = new Vector3[pathHolder[pathNumber].childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder[pathNumber].GetChild(i).position;
            //We change the y position of the waypoint to match the entity 
            waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
        }
        startWaypoint = waypoints[0];
    }
    public  void Init()
    {
        transform.position = waypoints[0];
        targetWaypointIndex = 1;
        targetWaypoint = waypoints[targetWaypointIndex];
    }
    public void FollowPath()
    {
        //We restart the navMeshAgent
        navMeshAgent.isStopped = false;
        navMeshAgent.destination = targetWaypoint;

        //Did the Entity is in the destination
        if (Vector3.Distance(transform.position, targetWaypoint) <= buffer)
        {
            Debug.Log("waypoint finish");
            //We go to the next waypoint
            if(targetWaypointIndex  >= waypoints.Length) //If we finished the path, we restart 
                targetWaypointIndex = 0;
            else 
                targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
            targetWaypoint = waypoints[targetWaypointIndex];
        }
    }
    public void FollowThisTransform(Transform transformToFollow)
    {
        Vector3 positionOfTheTransform = transformToFollow.position;

        //We restart the navMeshAgent
        navMeshAgent.isStopped = false;
        navMeshAgent.destination = positionOfTheTransform;
    }

    public IEnumerator SpeedManager()
    {
        float secondToWaitBeforeNextCheck = 1f;
        if(brain.IsTheEntityInTheDark())
        {
            if (navMeshAgent.speed < maxSpeed) navMeshAgent.speed += 0.01f;
        }
        else
        {
            if (navMeshAgent.speed > minSpeed) 
            { 
                navMeshAgent.speed -= 0.2f;
                secondToWaitBeforeNextCheck = 0.7f;
            }
        }
        yield return new WaitForSeconds(secondToWaitBeforeNextCheck);
        StartCoroutine(SpeedManager());
    }
    //
    //GIZMOS
    //
    private void OnDrawGizmos()
    {
        try
        {
            MakeGizmosPaths();
        }
        catch
        {

        }
    }
    private void MakeGizmosPaths()
    {
        foreach (Transform singlePathHolder in pathHolder)
        {
            Vector3 startPos = singlePathHolder.GetChild(0).position;
            Vector3 prevPos = startPos;
            foreach (Transform waypoint in singlePathHolder)
            {
                Gizmos.DrawSphere(waypoint.position, 0.3f);
                Gizmos.DrawLine(prevPos, waypoint.position);
                prevPos = waypoint.position;
            }
            Gizmos.DrawLine(prevPos, startPos);
        }
    }
}
