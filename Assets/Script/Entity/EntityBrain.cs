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
        entityPathfing.StartSpeedManager();
        StartCoroutine(ShouldIBreakTheLightInTheRoom());
    }
    private void Update()
    {
        currentState.StateUpdate();
        nameOfTheCurrentState = currentState.nameOhTheState.ToString();
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
    public bool IsTheEntityInTheDark()
    {
        bool value = true;
        Transform room = InWhitchRoomTheEntityIs();
        if(room.TryGetComponent<RoomScript>(out RoomScript script))
        {
            value = !script.AreTheLightOn;
        }
        return value;
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
    private IEnumerator ShouldIBreakTheLightInTheRoom()
    {
        yield return new WaitForSeconds(5f);
        if(IsTheEntityInTheDark())
        {
            float randomParameter = 1f;
            //if(nameOfTheCurrentState == EntityStates.EntityStates.Chase) randomParameter = 2f;
            if (UnityEngine.Random.value <= randomParameter)
            {
                //tkt
                Debug.LogWarning("DestroyingTheLight");
                StartCoroutine(InWhitchRoomTheEntityIs().GetComponent<RoomScript>().ShutDownAllLightInTheRoom());
            }
        }
        StartCoroutine(ShouldIBreakTheLightInTheRoom());
    }
}
