using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIP_PatrolBehaviour : AIP_FSMABase
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("patrol start");
        brain.SetColor(debugColor);
        brain.Movement.SetCanMove(true);

        brain.Patrol.FindRandomLocationInRange();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("patrol update");
        brain.Detection.Detect();

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("patrolexit");

        brain.Movement.SetCanMove(false);
    }

   
}
