using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStateStart : EntityState
{
    public EntityStateStart(EntityBrain entityBrain, EntityStateFactory stateFactory) : base(entityBrain, stateFactory)
    {
        nameOhTheState = EntityStates.Start;
    }
    public override void EnterState()
    {
        brain.entityPathfing.ChosePath(0);
        brain.entityPathfing.Init();
        SwitchState(factory.stateFollowPath());
    }

    public override void ExitState()
    {
        Debug.Log("The Entity is ready for moving");
    }

    public override void StateUpdate()
    {

    }
}
