using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStateInspecting : EntityState
{
    public EntityStateInspecting(EntityBrain entityBrain, EntityStateFactory stateFactory) : base(entityBrain, stateFactory)
    {
        nameOhTheState = EntityStates.Instpecting;
        //When the Entity listen a footsteps of the player runnings, they go see if the player is close
    }
    public override void EnterState()
    {
        Debug.Log("The Entity wears something");
    }

    public override void ExitState()
    {

    }

    public override void StateUpdate()
    {
        //Aller à la location du sound

        //Si on n'a pas trouver le joueur partir sur le chemin plus proche

    }
    private void CheckIfSwitchState()
    {
        if(brain.CanISeeThePlayer())
        {
            //SwitchState(ChasePlayerButNoSight)
        }
    }
}
