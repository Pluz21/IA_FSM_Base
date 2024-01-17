using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIP_IdleBehaviour : AIP_FSMABase
{

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("idlestart");
        brain.SetColor(debugColor);
        brain.Idle.GetRandomWaitingTime();                       //When we enter this state. We get a random float for our timer
        brain.Idle.StartTime();                                  // We start our timer with our maxtime and random time
    }

   
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("idleupdate");

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Debug.Log("idle exit");
    }


}
