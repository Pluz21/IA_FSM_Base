using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIP_MovementComponent : MonoBehaviour
{
    public event Action OnTargetReached = null;
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float rotationSpeed = 60;
    [SerializeField] bool canMove = false;
    [SerializeField] bool useTarget = false;        // This will allow me to pick where to go. Go to point? Go to target?
    [SerializeField] Vector3 patrolLocation = Vector3.zero;   //just a random point  from our patrol component
    [SerializeField] Transform enemyTransform = null;      // Enemy transform

    public bool IsAtLocation
    {
        get
        {
            Vector3 _otherPos = (useTarget && enemyTransform) ? enemyTransform.position : patrolLocation;
            if (Vector3.Distance(transform.position, _otherPos) <= 0.5f)
            { 
                if (useTarget && enemyTransform)
                    ResetUseTarget();
                return true;
            }
            return false;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetTarget(Transform _target)
    { 
        enemyTransform = _target;
        canMove = enemyTransform != null;   // if target, can move;
        useTarget = true;
    }

    void ResetUseTarget()
    { 
        useTarget = false;
    }
}
