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
        brain.redLight.SetActive(false);
        brain.entityPathfing.ChosePath(brain.entityPathfing.actualPath);
        brain.IsThePlayerGettingChase = false;
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
        //Did I Hear The Player ?
        //Yes SwitchState to Inspecting 
    }
}
