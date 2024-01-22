using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NM_Brain : MonoBehaviour

{
    [SerializeField] Animator fsm = null;
    [SerializeField] NM_Idle idle = null;
    [SerializeField] NM_Navigation navigation = null;
    Color debugColor = Color.white;
    Behaviour[] behaviours = new Behaviour[0];

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
    }

    void Update()
    {
        
    }
}
