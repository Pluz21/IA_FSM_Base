using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EatComponent),typeof(IdleComponent),typeof(MovementComponent))]

public class FSMABrain : MonoBehaviour
{
    public static readonly int IDLE_DONE = Animator.StringToHash("idlingDone");
    public static readonly int FINDTARGET_DONE= Animator.StringToHash("findingTargetDone");
    public static readonly int FINDRANDOMPOINT_DONE= Animator.StringToHash("findingRandomPointDone");
    public static readonly int EATING_DONE= Animator.StringToHash("eatingDone");
    public static readonly int MOVING_DONE = Animator.StringToHash("movingDone");
    [SerializeField] Animator fsm = null;
    [SerializeField] EatComponent eating = null;
    [SerializeField] IdleComponent idling = null;
    [SerializeField] MovementComponent moving = null;
    [SerializeField] LookForEnemyComponent lookingForEnemy = null;
    FSMABase[] behaviours = new FSMABase[0];
    public EatComponent Eating => eating;
    public IdleComponent Idling => idling;
    public MovementComponent Moving => moving;
    public LookForEnemyComponent LookingForEnemy => lookingForEnemy;

    Color debugColor = Color.white;
    void Start()
    {
        InitFsm();    
    }

    void InitFsm()
    { 
        fsm = GetComponent<Animator>();
        eating = GetComponent<EatComponent>();
        idling = GetComponent<IdleComponent>();
        lookingForEnemy = GetComponent<LookForEnemyComponent>();
        moving = GetComponent<MovementComponent>();

         idling.OnElapsed += () =>
         {
             if (!eating.IsValid) return;
             fsm.SetBool(IDLE_DONE, true);
             fsm.SetBool(EATING_DONE, false);
         };
        eating.OnSelect += (t) =>
        {
            moving.SetTarget(t);
            fsm.SetBool(FINDTARGET_DONE, false);
        };
        moving.OnTargetReached += () =>
        {
            eating.Eat();
            fsm.SetBool(IDLE_DONE, false);
            fsm.SetBool(EATING_DONE, true);
        };
        lookingForEnemy.OnEnemyInRange += () =>
        {
            fsm.SetBool(FINDTARGET_DONE, true);
        };
        if (!fsm) return;
        behaviours = fsm.GetBehaviours<FSMABase>();    // returns an array of behaviours, which is a component of a StateMachine
        int _size = behaviours.Length;
        for (int i = 0; i < _size; i++)
        {
            behaviours[i].Init(this);
        }
    }

    void Update()
    {
        
    }

    public void SetColor(Color _color)
    {
        debugColor = _color;
    }

    private void OnDrawGizmos()
    {
        AnmaGizmos.DrawSphere(transform.position + Vector3.up, 0.8f, debugColor,AnmaGizmos.DrawMode.Full);
        if(moving)
        AnmaGizmos.DrawSphere(transform.position, 7, debugColor, AnmaGizmos.DrawMode.Wire);

    }
}
