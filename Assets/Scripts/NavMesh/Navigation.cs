using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField] NavMeshAgent agent = null;
    [SerializeField] List<Vector3> path = null;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        NavMeshPath _path = new NavMeshPath();
        // agent.autoRepath = true;
        NavMesh.CalculatePath(transform.position, target.position, 1, _path);
        path = _path.corners.ToList();
        InitPath();

    }

    void InitPath()
    {
        NavMeshPath _path = new NavMeshPath();
        agent.CalculatePath(target.position, _path);
        if (_path.status == NavMeshPathStatus.PathPartial)
            Invoke(nameof(InitPath), 0.5f);
        path = _path.corners.ToList();
    }
    void Update()
    {
              
    }

    private void FixedUpdate()
    {
        if (!agent) return;
        if (!agent.enabled || !target) return;
        agent.SetDestination(target.position);
        //agent.SetPath();
        //NavMesh.CalculatePath();

    }

    private void OnCollisionEnter(Collision collision)
    {
       if (collision.gameObject.layer != 6) return;
        agent.enabled = false;
        Debug.Log("entered collision with navmesh target");
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
