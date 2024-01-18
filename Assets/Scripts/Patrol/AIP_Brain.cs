using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[RequireComponent(typeof(AIP_AttackComponent),typeof(AIP_IdleComponent),typeof(AIP_PatrolComponent))]
[RequireComponent(typeof(AIP_MovementComponent),typeof(Animator),typeof(AIP_DetectionComponent))]
public class AIP_Brain : MonoBehaviour
{
    public static readonly int IDLE_DONE = Animator.StringToHash("idleDone");
    public static readonly int PATROL_DONE = Animator.StringToHash("patrolDone");
    public static readonly int CHASE_DONE = Animator.StringToHash("chaseDone");
    public static readonly int ATTACK_DONE = Animator.StringToHash("attackDone");
    [SerializeField] Animator fsm = null;
    [SerializeField] AIP_IdleComponent idle = null;
    [SerializeField] AIP_PatrolComponent patrol = null;
    [SerializeField] AIP_AttackComponent attack = null;
    [SerializeField] AIP_MovementComponent movement = null;
    [SerializeField] AIP_DetectionComponent detection = null;
    
    Color debugColor = Color.white;

    AIP_FSMABase[] behaviours = new AIP_FSMABase[0];

    public Animator Fsm => fsm;
    public AIP_IdleComponent Idle => idle;
    public AIP_PatrolComponent Patrol => patrol;
    public AIP_AttackComponent Attack => attack;
    public AIP_MovementComponent Movement => movement;
    public AIP_DetectionComponent Detection => detection;

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
        detection = GetComponent<AIP_DetectionComponent>();

        if (!IsValid) return;     // We do a check here because we want to subscribe events once they are valid. 

        idle.OnElapsed += () =>                                 // We go to STATE PATROL and call patrol function
        {
            fsm.SetBool(IDLE_DONE, true);  
            fsm.SetBool(PATROL_DONE, false);     // We set the patrol bool of our animator here to false, just to make sure everytime our 
                                                // animator is in the IDLE state, it also resets the patrol done bool
        };
        patrol.OnRandomLocationFound += (t) =>          // While in state patrol, finds a random location
                                                        // thenwe set the location for the ai to move to
        {
            movement.SetPatrolLocation(t);              
        };
        movement.OnTargetReached += () =>
        {
            fsm.SetBool(IDLE_DONE, false);
            fsm.SetBool(PATROL_DONE, true);
            // Temp
            // Temp
        };
        attack.OnIsInRange += (b) =>                        // This event is called in update.
        {
            if (!b)
            {
                fsm.SetBool(ATTACK_DONE, true);
                fsm.SetBool(CHASE_DONE, false);
                return;
            }
            fsm.SetBool(ATTACK_DONE, false);
            fsm.SetBool(CHASE_DONE, true);
            movement.SetCanMove(false);
        };
        attack.OnTargetDestroyed += (g) =>
        {
            fsm.SetBool(ATTACK_DONE, true);
            fsm.SetBool(IDLE_DONE, false);
            detection.RemoveEntity(g);
        };
        detection.OnEntityDetected += (e) =>
        {
            Debug.Log("OnDetected");
            if (!e)
            {
                fsm.SetBool(PATROL_DONE, false);
                fsm.SetBool(CHASE_DONE, true);
                movement.SetTarget(null);
                attack.SetTarget(null);
                return;
            }
            fsm.SetBool(PATROL_DONE, true);
            fsm.SetBool(CHASE_DONE,false);
            attack.SetTarget(e.transform);
            movement.SetTarget(e.transform);
        };
        behaviours = fsm.GetBehaviours<AIP_FSMABase>();      //Returns all StateMachineBehaviour that
                                                            //match type T or are derived from T. Returns null if none are found.
        int _size = behaviours.Length;
        for (int i = 0; i < _size; i++)
        {
            behaviours[i].Init(this);       // no check needed since we already call GetBehaviours earlier. 
                                            
                                           
        }
    }

    public void SetColor(Color _color)
    {
        debugColor = _color;
    }

    private void OnDrawGizmos()
    {
        AnmaGizmos.DrawSphere(transform.position + (transform.up * 1.5f), 0.5f, debugColor,AnmaGizmos.DrawMode.Full);
    }
}
