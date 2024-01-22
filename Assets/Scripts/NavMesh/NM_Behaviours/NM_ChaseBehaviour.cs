using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NM_ChaseBehaviour : NM_FSMA
{
    [SerializeField] float current = 0;
    [SerializeField] float max= 0.5f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        brain.SetColor(debugColor);
        brain.Agent.SetDestination(brain.Navigation.Target.position);
        brain.Agent.enabled = true;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!brain.Agent.enabled)
            UpdateTime(ref current, max);
    }

    private void UpdateTime(ref float _current, float _max)
    {
        _current += Time.deltaTime;
        if(_current >= _max)
        {
            _current = 0;
            brain.Agent.SetDestination(brain.Navigation.Target.position);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
