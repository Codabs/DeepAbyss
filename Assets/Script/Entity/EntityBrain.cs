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

    [Header("State Machine Variable")]
    public EntityState currentState;
    private EntityStateFactory factory;
    public string nameOfTheCurrentState;

    [Header("Ref")]
    [SerializeField] private Transform player;

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
        currentState = factory.stateStart();
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
    public void CanIHearThePlayer()
    {

    }
}
