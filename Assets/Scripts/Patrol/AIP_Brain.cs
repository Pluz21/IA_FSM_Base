using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIP_AttackComponent),typeof(AIP_IdleComponent),typeof(AIP_PatrolComponent))]
[RequireComponent(typeof(AIP_MovementComponent),typeof(Animator))]
public class AIP_Brain : MonoBehaviour
{
    [SerializeField] Animator fsm = null;
    [SerializeField] AIP_IdleComponent idle = null;
    [SerializeField] AIP_PatrolComponent patrol = null;
    [SerializeField] AIP_AttackComponent attack = null;
    [SerializeField] AIP_MovementComponent movement = null;
    Color debugColor = Color.white;

    AIP_FSMABase[] behaviours = new AIP_FSMABase[0];

    public Animator Fsm => fsm;
    public AIP_IdleComponent Idle => idle;
    public AIP_PatrolComponent Patrol => patrol;
    public AIP_AttackComponent Attack => attack;
    public AIP_MovementComponent Movement => movement;

    public bool IsValid => fsm && idle && patrol && movement && attack; // We want all 
    void Start()
    {
        InitFSM();
    }

    void Update()
    {
        
    }

    void InitFSM()
    { 
        fsm = GetComponent<Animator>();
        idle = GetComponent<AIP_IdleComponent>();
        attack = GetComponent<AIP_AttackComponent>();
        movement = GetComponent<AIP_MovementComponent>();
        patrol = GetComponent<AIP_PatrolComponent>();

        if (!IsValid) return;     // We do a check here because we want to subscribe events once they are valid. 
        behaviours = fsm.GetBehaviours<AIP_FSMABase>();      //Returns all StateMachineBehaviour that
                                                            //match type T or are derived from T. Returns null if none are found.
        int _size = behaviours.Length;
        for (int i = 0; i < _size; i++)
        {
            behaviours[i].Init(this);       // no check needed since we already call GetBehaviours earlier. 
                                           // 
                                           
        }
    }

    public void SetColor(Color _color)
    {
        debugColor = _color;
    }

    private void OnDrawGizmos()
    {
        AnmaGizmos.DrawSphere(transform.position + (transform.up * 1.5f), 0.5f, debugColor);
    }
}
