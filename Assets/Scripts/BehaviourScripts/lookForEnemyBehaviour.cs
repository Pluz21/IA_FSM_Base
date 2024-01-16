using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookForEnemyBehaviour : FSMABase
{    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!brain) return;
        Debug.Log("Entered LookingForEnemy State");
        brain.SetColor(debugColor);
        //brain.LookingForEnemy.SetTarget();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
