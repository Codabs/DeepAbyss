using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStateFactory
{
    private Dictionary<EntityState.EntityStates, EntityState> states;
    readonly EntityBrain brain;
    public EntityStateFactory(EntityBrain entityBrain)
    {
        brain = entityBrain;

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
