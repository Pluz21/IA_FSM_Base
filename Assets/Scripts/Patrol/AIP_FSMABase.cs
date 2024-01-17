using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIP_FSMABase : StateMachineBehaviour
{
    [SerializeField] protected AIP_Brain brain = null;
    [SerializeField] protected Color debugColor = Color.black;    // if fsm wowrks, white. Otherwise black; 
  

    public void Init(AIP_Brain _brain)   // We make it public so that each behaviour can get a reference to the brain                         
    { 
        brain = _brain;
    }
}
