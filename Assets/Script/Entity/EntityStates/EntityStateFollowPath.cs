using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStateFollowPath : EntityState
{
    public EntityStateFollowPath(EntityBrain entityBrain, EntityStateFactory stateFactory) : base(entityBrain, stateFactory)
    {

    }
    public override void EnterState()
    {
        //Go the closest waypoint
    }
    public override void ExitState()
    {

    }

    public override void StateUpdate()
    {
        brain.entityPathfing.FollowPath();
        CheckIfSwitchState();
    }
    private void CheckIfSwitchState()
    {
        //DidISeeThePlayer?
        if(brain.CanISeeThePlayer())
        {
            //Yes SwitchState to Chase
            SwitchState(factory.GetAnyState(EntityStates.ChasePlayer));
        }
        else if(brain.CanIHearThePlayer())
        {
            SwitchState(factory.GetAnyState(EntityStates.Instpecting));
        }
        //Did I Hear The Player ?
        //Yes SwitchState to Inspecting 
    }
}
