using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NM_FSMA : StateMachineBehaviour
{
    [SerializeField] protected NM_Brain brain = null;
    [SerializeField] protected Color debugColor = Color.black;

    public Color DebugColor => debugColor;
    public void Init(NM_Brain _brain)
    {
        brain = _brain;
    }
    //public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) // here we can check 
    // everything that is inside the AnimatorStateInfo, and use it to create our own behaviours.
    //{
        
    //}
}
