using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStateChasePlayerButLoseSight : EntityState
{
    private float timer = 0;
    public EntityStateChasePlayerButLoseSight(EntityBrain entityBrain, EntityStateFactory stateFactory) : base(entityBrain, stateFactory)
    {
        nameOhTheState = EntityStates.ChasePlayerButILoseSight;
    }
    public override void EnterState()
    {
        //On reset le timer
        timer = 0;
    }

    public override void ExitState()
    {

    }

    public override void StateUpdate()
    {
        //We start a timer of 10 sec, after that, if the entity didn't see the player, the go pack on follow path
        if(timer > 25)
        {
            SwitchState(factory.GetAnyState(EntityStates.FollowPath));
        }
        else
        {
            timer += Time.deltaTime;
        }
        CheckIfSwitchState();
        //The Entity have to follow the player
        brain.entityPathfing.FollowThisTransform(brain.player);
    }
    private void CheckIfSwitchState()
    {
        if(brain.CanISeeThePlayer())
        {
            SwitchState(factory.GetAnyState(EntityStates.ChasePlayer));
        }
    }
}
