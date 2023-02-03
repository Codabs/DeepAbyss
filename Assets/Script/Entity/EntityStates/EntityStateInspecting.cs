using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStateInspecting : EntityState
{
    //
    //Variables
    //
    private Transform positionOfTheSound = null;
    public float buffer = 0.5f;
    public EntityStateInspecting(EntityBrain entityBrain, EntityStateFactory stateFactory) : base(entityBrain, stateFactory)
    {
        nameOhTheState = EntityStates.Instpecting;
        //When the Entity listen a footsteps of the player runnings, they go see if the player is close
    }
    public override void EnterState()
    {
        Debug.Log("The Entity wears something");
        //We create a Transform
        positionOfTheSound = new GameObject("oldPositionOfThePlayer").transform;

        positionOfTheSound.position = brain.positionOfTheSound;
    }

    public override void ExitState()
    {
        positionOfTheSound = null;
    }

    public override void StateUpdate()
    {
        //Aller à la location du sound
        brain.entityPathfing.FollowThisTransform(positionOfTheSound);
        if(brain.CanIHearThePlayer())
        {
            positionOfTheSound.position = brain.positionOfTheSound;
        }
        //Si on n'a pas trouver le joueur partir sur le chemin plus proche
        CheckIfSwitchState();
    }
    private void CheckIfSwitchState()
    {
        if(brain.CanISeeThePlayer())
        {
            SwitchState(factory.GetAnyState(EntityStates.ChasePlayer));
        }
        if(Vector3.Distance(brain.transform.position, positionOfTheSound.position) <= buffer)
        {
            SwitchState(factory.GetAnyState(EntityStates.FollowPath));
        }   
    }
}
