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
    }

    public override void ExitState()
    {

    }

    public override void StateUpdate()
    {
        brain.entityPathfing.FollowPath();
    }
    private void CheckIfSwitchState()
    {
        //DidISeeThePlayer?
        if(brain.CanISeeThePlayer())
        {
            //Yes SwitchState to Chase
            //SwitchState()
        }
        else if(brain.CanISeeThePlayer())
        {

        }
        //Did I Hear The Player ?
        //Yes SwitchState to Inspecting 
    }
}
