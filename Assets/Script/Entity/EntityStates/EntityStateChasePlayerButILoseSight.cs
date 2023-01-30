using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStateChasePlayer : EntityState
{
    public EntityStateChasePlayer(EntityBrain entityBrain, EntityStateFactory stateFactory) : base(entityBrain, stateFactory)
    {
        nameOhTheState = EntityStates.ChasePlayer;
    }
    public override void EnterState()
    {

    }

    public override void ExitState()
    {

    }

    public override void StateUpdate()
    {

    }
    private void CheckIfSwitchState()
    {
        if(!brain.CanISeeThePlayer())
        {
            //SwitchState(ChasePlayerButNoSight)
        }
    }
}
