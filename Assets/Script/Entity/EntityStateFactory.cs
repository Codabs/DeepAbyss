using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStateFactory
{
    private Dictionary<EntityState.EntityStates, EntityState> states = new Dictionary<EntityState.EntityStates, EntityState>();
    readonly EntityBrain brain;
    public EntityStateFactory(EntityBrain entityBrain)
    {
        brain = entityBrain;
        //We create all the state 
        states.Add(EntityState.EntityStates.NotActive, new EntityStateNotActive(brain, this));
        states.Add(EntityState.EntityStates.Start, new EntityStateStart(brain, this));
        states.Add(EntityState.EntityStates.FollowPath, new EntityStateFollowPath(brain, this));
        states.Add(EntityState.EntityStates.ChasePlayer, new EntityStateChasePlayer(brain, this));
        states.Add(EntityState.EntityStates.ChasePlayerButILoseSight, new EntityStateChasePlayerButLoseSight(brain, this));
        states.Add(EntityState.EntityStates.Instpecting, new EntityStateInspecting(brain, this));
        //states.Add(EntityState.EntityStates.GameOver, new EntityStateGameover(brain, this));
    }
    public EntityState GetAnyState(EntityState.EntityStates stateName)
    {
        if (states.TryGetValue(stateName, out EntityState stateScript))
        {
            return stateScript;
        }
        else
        {
            Debug.LogWarning("StateNotFind");
            return null;
        }
    }
    public EntityStateStart stateStart()
    {
        return new EntityStateStart(brain, this);
    }
    public EntityStateFollowPath stateFollowPath()
    {
        return new EntityStateFollowPath(brain, this);
    }
    public EntityStateChasePlayer chasePlayer()
    {
        return new EntityStateChasePlayer(brain, this);
    }
}
