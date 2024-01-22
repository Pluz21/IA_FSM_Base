using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NM_Navigation : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent = null;
    [SerializeField] Transform target = null;

    public Transform Target => target;
    void Start()
    {
        Init();
    }
    public void ManageNavigation(bool _value)
    {
        agent.enabled = _value;
     
    }

    void Init()
    { 
        agent = GetComponent<NavMeshAgent>();
        if (!target) return;
        //agent.SetDestination(target.position);
    }

    public void SetTarget(Transform _target)
    { 
        target = _target;
    }
    void Update()
    {
        
    }
}
