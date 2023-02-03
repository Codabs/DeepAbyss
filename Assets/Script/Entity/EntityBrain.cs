using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBrain : MonoBehaviour
{
    //
    //VARIABLE
    //

    [Header("Entity Component")]
    public EntityPathfing entityPathfing;
    public EntityEyes entityEyes;
    public EntityListener entityListener;
    public EntityBody entityBody;
    public new Rigidbody rigidbody;

    [Header("State Machine Variable")]
    public EntityState currentState;
    private EntityStateFactory factory;
    public string nameOfTheCurrentState;

    [Header("Ref")]
    public Transform player;

    [Header("speed")]
    [SerializeField] private List<Transform> roomList = new();
    private KdTree<Transform> roomKdTree = new();
    public bool IsThePlayerFlashingTheEntity = false;

    public GameObject redLight;
    public float whenWasTheLastTimeTheEntityMeetThePlayer = 1;
    public Vector3 positionOfTheSound = Vector3.zero;
    public bool IsThePlayerGettingChase = false;
    //
    //MONOBEHAVIOUR
    //
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        roomKdTree.AddAll(roomList);
    }
    private void Start()
    {
        factory = new EntityStateFactory(this);
        currentState = factory.GetAnyState(EntityState.EntityStates.Start);
        currentState.EnterState();
        //entityPathfing.StartSpeedManager();
        //StartCoroutine(ShouldIBreakTheLightInTheRoom());
    }
    private void Update()
    {
        currentState.StateUpdate();
        nameOfTheCurrentState = currentState.nameOhTheState.ToString();
        if(entityPathfing.navMeshAgent.speed < 10)
            entityPathfing.navMeshAgent.speed += 0.05f;
    }
    //
    //FONCTION
    //
    public bool CanISeeThePlayer()
    {
        return entityEyes.CanISeeThisTransform(player);
    }
    public bool CanIHearThePlayer()
    {
        return false;
    }

    /*
    public bool IsTheEntityInTheDark()
    {
        Transform room = InWhitchRoomTheEntityIs();
        try
        {
            if (room.TryGetComponent<RoomScript>(out RoomScript script))
            {
                return !script.AreTheLightOn;
            }
            else
            {
                return true;
            }
        }
        catch
        {
            return true;
        }
    }
    public Transform InWhitchRoomTheEntityIs()
    {
        Transform roomTHeEntityIs = null;
        try
        {
            roomTHeEntityIs = roomKdTree.FindClosest(transform.position);
        }
        catch
        {
            Debug.Log("roomNotFound");
        }
        return roomTHeEntityIs;
    }
    public void BreakTheLightOnTheCurrentRoom()
    {
        if (InWhitchRoomTheEntityIs() == null) return;
        try
        {
            InWhitchRoomTheEntityIs().GetComponent<RoomScript>().ShutDownAllLightInTheRoom();
        }
        catch { }
    }
    private IEnumerator ShouldIBreakTheLightInTheRoom()
    {
        yield return new WaitForSeconds(10f);
        if(IsTheEntityInTheDark())
        {
            float randomParameter = 0.5f;
            if (UnityEngine.Random.value <= randomParameter)
            {
                //tkt
                Debug.LogWarning("DestroyingTheLight");
                InWhitchRoomTheEntityIs().GetComponent<RoomScript>().ShutDownAllLightInTheRoom();
            }
        }
        StartCoroutine(ShouldIBreakTheLightInTheRoom());
    }
    */
}
