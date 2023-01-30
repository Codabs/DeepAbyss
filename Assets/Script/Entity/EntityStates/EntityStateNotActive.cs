using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStateNotActive : EntityState
{
    public EntityStateNotActive(EntityBrain entityBrain, EntityStateFactory stateFactory) : base(entityBrain, stateFactory)
    {
        nameOhTheState = EntityStates.NotActive;
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
}
