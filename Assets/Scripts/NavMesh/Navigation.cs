using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{
    public event Action OnFinishedCollision;
    [SerializeField] Transform target = null;
    [SerializeField] Transform temporaryTarget = null;
    [SerializeField] NavMeshAgent agent = null;
    [SerializeField] List<Vector3> path = null;
    [SerializeField] bool canStartNav  = false ;
    public bool CanStartNav
    {
        get {
            return canStartNav;
            }
        set
        {
            canStartNav = value;
        }
    
    }

    void Start()
    {
        //temporaryTarget = target;
        //Transform _tempTarget = temporaryTarget;
       // target = _tempTarget; //
        
        //agent = GetComponent<NavMeshAgent>();
        //NavMeshPath _path = new NavMeshPath();
        //// agent.autoRepath = true;
        //NavMesh.CalculatePath(transform.position, target.position, 1, _path);
        //path = _path.corners.ToList();
        //InitPath();

    }

    public void InitPath()
    {
        if (!canStartNav)
        {
            //target = null;
             return;
        }
        NavMeshPath _path = new NavMeshPath();   
        agent.CalculatePath(target.position, _path);
        if (_path.status == NavMeshPathStatus.PathPartial)
            Invoke(nameof(InitPath), 0.5f);
        path = _path.corners.ToList();
        agent.SetDestination(target.position);

    }

    public void SetCanStartNav(bool _value)
    { 
        canStartNav = _value;
    }

    void Update()
    {
              
    }

    private void FixedUpdate()
    {
        if (!agent) return;
        if (!agent.enabled || !target || !canStartNav) return;
        agent.SetDestination(target.position);
        //agent.SetPath();
        //NavMesh.CalculatePath();

    }

     void OnCollisionEnter(Collision collision)
    {
       if (collision.gameObject.layer != 6) return;
        agent.enabled = false;
        Debug.Log("entered collision with navmesh target");
        OnFinishedCollision?.Invoke();
    }
    private void OnCollisionExit(Collision collision)
    {
       if (collision.gameObject.layer != 6) return;
       agent.enabled = true;
        
    }

    private void OnDrawGizmos()
    {
        if (path.Count < 1) return;
        for (int i = 0; i < path.Count; i++) 
        {
            AnmaGizmos.DrawSphere(path[i], 0.5f, TVL_Colors.Colors.CoralPink);
            if (i + 1 < path.Count)
                AnmaGizmos.DrawLine(path[i], path[i + 1],TVL_Colors.Colors.CoralPink);
        }
        
    }
}
