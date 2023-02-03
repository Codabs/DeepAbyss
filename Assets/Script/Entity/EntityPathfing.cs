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
    public float maxSpeed = 10f;
    public float minSpeed = 3f;

    [Header("Component")]
    public NavMeshAgent navMeshAgent;
    [SerializeField] private EntityBrain brain;

    //Script variable
    private Transform[] waypoints;
    public KdTree<Transform> waypointsKdTree = new();
    private Vector3 startWaypoint;
    int targetWaypointIndex;
    private Vector3 targetWaypoint;
    public int actualPath = 0;
    //
    //FONCTION
    //
    public void ChosePath(int pathNumber)
    {
        //We check if the pathNumber is valid
        if (pathNumber > pathHolder.Length) return;

        //We get all the waypoints in the path
        waypoints = new Transform[pathHolder[pathNumber].childCount];

        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder[pathNumber].GetChild(i);
            //We change the y position of the waypoint to match the entity 
            //NavMesh.SamplePosition(waypoints[i].position, out NavMeshHit navHit, 10, 0);
            waypoints[i].position = waypoints[i].position;
        }
        startWaypoint = waypoints[0].position;
    }
    public  void Init()
    {
        transform.position = waypoints[0].position;
        targetWaypointIndex = 1;
        targetWaypoint = waypoints[targetWaypointIndex].position;
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
            /*if(targetWaypointIndex  >= waypoints.Length) //If we finished the path, we restart 
                targetWaypointIndex = 0;
            else */
                targetWaypointIndex = Mathf.RoundToInt(UnityEngine.Random.Range(0, waypoints.Length - 1));
            targetWaypoint = waypoints[targetWaypointIndex].position;
        }
    }
    public void FollowThisTransform(Transform transformToFollow)
    {
        Vector3 positionOfTheTransform = transformToFollow.position;

        //We restart the navMeshAgent
        navMeshAgent.isStopped = false;
        navMeshAgent.destination = positionOfTheTransform;
    }
    public void StartSpeedManager()
    {
        StartCoroutine(SpeedManager());
    }
    public IEnumerator SpeedManager()
    {
        float secondToWaitBeforeNextCheck = 0.3f;
        if (navMeshAgent.speed < maxSpeed) navMeshAgent.speed += 0.2f;
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
