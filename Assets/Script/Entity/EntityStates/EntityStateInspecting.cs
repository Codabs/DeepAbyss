using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStateInspecting : EntityState
{
    //
    //Variables
    //
    private Transform positionOfTheSound = null;
    public float buffer = 0.5f;
    public EntityStateInspecting(EntityBrain entityBrain, EntityStateFactory stateFactory) : base(entityBrain, stateFactory)
    {
        nameOhTheState = EntityStates.Instpecting;
        //When the Entity listen a footsteps of the player runnings, they go see if the player is close
    }
    public override void EnterState()
    {
    }

    public override void ExitState()
    {
        positionOfTheSound = null;
    }

    public override void StateUpdate()
    {
        CheckIfSwitchState();
    }
    private void CheckIfSwitchState()
    {
    }
}
