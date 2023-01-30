using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityState
{
    //
    //VARIABLE
    //

    //We get a ref of the other state (factory) and the statemachine (brain)
    public EntityBrain brain;
    public EntityStateFactory factory;

    //For editor mode, so we have the name of the current state in the state machine
    public enum EntityStates
    {
        Null, NotActive, Start, FollowPath, Stoping, Instpecting, ChasePlayer, ChasePlayerButILoseSight, GameOver
    }
    public EntityStates nameOhTheState = 0;
    public EntityState(EntityBrain entityBrain, EntityStateFactory entityStateFactory)
    {
        brain = entityBrain;
        factory = entityStateFactory;
    }
    public abstract void EnterState();
    public abstract void StateUpdate();
    public abstract void ExitState();
    public void SwitchState(EntityState newState)
    {
        //We execute the end state and enter of the new State
        ExitState();

        newState.EnterState();

        //We change the current state in the statemachine (brain)
        brain.currentState = newState;
    }
}
