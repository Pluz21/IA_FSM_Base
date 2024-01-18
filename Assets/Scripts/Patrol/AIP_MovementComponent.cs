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
        MoveTo();
        RotateTo();
    }

    void RotateTo()
    {
        if (!canMove) return;
        Vector3 _otherPos = (useTarget && enemyTransform) ? enemyTransform.position : patrolLocation;
        Vector3 _look = _otherPos - transform.position;
        if (_look == Vector3.zero) return;
        Quaternion _rot = Quaternion.LookRotation(_look);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _rot, Time.deltaTime * rotationSpeed);
    }

    void MoveTo()
    {
        if (!canMove) return;
        Vector3 _otherPos = (useTarget && enemyTransform) ? enemyTransform.position : patrolLocation;
        transform.position = Vector3.MoveTowards(transform.position, _otherPos, Time.deltaTime * moveSpeed);
        if(IsAtLocation)
            OnTargetReached?.Invoke();
    }


    public void SetTarget(Transform _target)
    {
        Debug.Log($"SetTarget called, target is {_target}");
        enemyTransform = _target;
        canMove = _target != null;   // if target, can move;
        useTarget = _target != null;
        canMove = true; //removed. might need to reset 
    }

    public void SetPatrolLocation(Vector3 _pos)
    { 
        patrolLocation = _pos;
    }

    void ResetUseTarget()
    {
        enemyTransform = null;
        useTarget = false;
        canMove = false;
    }

    public void SetCanMove(bool _value)
    { 
        canMove = _value;
    }
}
