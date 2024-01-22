using System;
using System.Collections;
using UnityEngine;

public class NM_Brain : MonoBehaviour

{
    [SerializeField] Animator fsm = null;
    [SerializeField] NM_Idle idle = null;
    [SerializeField] NM_Navigation navigation = null;
    Color debugColor = Color.white;
    NM_FSMA[] behaviours = new NM_FSMA[0];

    public Animator FSM => fsm;
    public NM_Idle Idle => idle;
    public NM_Navigation Navigation => navigation;

    public bool IsValid => fsm != null && idle && navigation;


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
        if (!IsValid) return;


        idle.OnElapsed += () =>
        {

        };

        //behaviours = fsm.GetBehaviours<NM_FSMA>();
        int _size = behaviours.Length;
        for (int i = 0; i < _size; i++)
        {
            behaviours[i].Init(this);
        }
    }

    void Update()
    {
        
    }
}
