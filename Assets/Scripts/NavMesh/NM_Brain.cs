using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NM_Brain : MonoBehaviour

{

    public event Action<bool> OnCollision;
    public static readonly int IDLE_DONE = Animator.StringToHash("idleDone");
    public static readonly int CHASE_DONE = Animator.StringToHash("chaseDone");
    [SerializeField] Animator fsm = null;
    [SerializeField] NM_Idle idle = null;
    [SerializeField] NM_Navigation navigation = null;
    [SerializeField] NavMeshAgent agent = null;
    Color debugColor = Color.white;
    NM_FSMA[] behaviours = new NM_FSMA[0];

    public Animator FSM => fsm;
    public NM_Idle Idle => idle;
    public NM_Navigation Navigation => navigation;

    public NavMeshAgent Agent => agent;
    public bool IsValid => fsm && idle && navigation;


    // basic setup for the brain done
    void Start()
    {
        InitFSM();
    }

    private void InitFSM()
    {
        fsm = GetComponent<Animator>();
        idle = GetComponent<NM_Idle>();
        navigation = GetComponent<NM_Navigation>();
        agent = GetComponent<NavMeshAgent>();
        behaviours = fsm.GetBehaviours<NM_FSMA>();

        if (!IsValid) return;
        int _size = behaviours.Length;
        for (int i = 0; i < _size; i++)
        {
            Debug.Log("Init the  behaviours");
            behaviours[i].Init(this);
        }


        idle.OnElapsed += () =>
        {
           if (!agent.enabled)
            { 
            Debug.Log("elapsed done");
                idle.GetRandomWaitingTime();
                idle.StartTime();
                return;
            }
            fsm.SetBool(IDLE_DONE, true);
            fsm.SetBool(CHASE_DONE, false);
            
            

        };

        OnCollision += (b) =>
        {
            if (b)
            { 
            fsm.SetBool(IDLE_DONE, false);
            fsm.SetBool(CHASE_DONE, true);
            }
            navigation.ManageNavigation(!b);
        };
    }

    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
   
        if (!collision.transform.GetComponent<NM_Target>()) return;
        // if (!collision.transform.TryGetComponent<NM_Target>(out NM_Target _target)) return; with an out
        OnCollision?.Invoke(true);

    }

    private void OnCollisionExit(Collision collision)
    {
        if (!collision.transform.GetComponent<NM_Target>()) return;
        OnCollision?.Invoke(false);
        
    }

    public void SetColor(Color _color)
    {
        debugColor = _color;

    }
    private void OnDrawGizmos()
    {
       //AnmaGizmos.DrawSphere(transform.up , 0.9f, TVL_Colors.Colors.DarkKhaki);
        AnmaGizmos.DrawSphere(transform.position + (transform.up * 1.5f), 0.5f, debugColor, AnmaGizmos.DrawMode.Full);

    }

}
