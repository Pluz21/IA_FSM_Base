using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class FSMABase : StateMachineBehaviour
{
    [SerializeField] protected FSMABrain brain = null;
    [SerializeField] protected Color debugColor = Color.black;    

    public Color DebugColor => debugColor;
 

    public void Init(FSMABrain _brain)
    {
        brain = _brain;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        brain.SetColor(debugColor);
        base.OnStateEnter(animator, stateInfo, layerIndex);

    }
}

