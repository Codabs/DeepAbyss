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
    [SerializeField] private KdTree<Transform> roomList = new();

    //
    //MONOBEHAVIOUR
    //
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Start()
    {
        factory = new EntityStateFactory(this);
        currentState = factory.GetAnyState(EntityState.EntityStates.NotActive);
        currentState.EnterState();
    }
    private void Update()
    {
        currentState.StateUpdate();
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
        bool value = false;
        Transform room = InWhitchRoomTheEntityIs();
        if(room.TryGetComponent<RoomScript>(out RoomScript script))
        {
            value = script.AreTheLightOn;
        }
        return value;
    }
    public Transform InWhitchRoomTheEntityIs()
    {
        Transform roomTHeEntityIs = null;
        try
        {
            roomTHeEntityIs = roomList.FindClosest(transform.position);
        }
        catch
        {
        }
        return roomTHeEntityIs;
    }
    public void StartTheSpeedManager()
    {
        StartCoroutine(entityPathfing.SpeedManager());
    }
    public void SpawnTheEntity()
    {
        currentState.SwitchState(factory.GetAnyState(EntityState.EntityStates.Start));
    }
}
