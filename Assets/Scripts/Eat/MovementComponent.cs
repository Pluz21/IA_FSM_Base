using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    public event Action OnTargetReached;
    [SerializeField] float moveSpeed = 10, rotationSpeed = 50;
    [SerializeField] bool canMove = true;
    [SerializeField] Transform target = null;
    [SerializeField] float minDistance = 0.5f;

    public bool IsAtLocation
    {
        get { 
            if (!target) return true;
            if (Vector3.Distance(transform.position, target.position) < minDistance)
                return true;
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

    public void SetTarget(Transform _target)
    { 
        target = _target;
        canMove = _target != null;    // if (_target != null) canMove = true;
    }

    public void MoveTo()
    {
        if (!canMove || !target) return;
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);
        if (IsAtLocation) 
        {
            OnTargetReached?.Invoke();
        }

        
    }

    public void RotateTo()
    {
        if (!canMove || !target || IsAtLocation ) return;
        Vector3 _lookDirection = target.position - transform.position;
        if (_lookDirection == Vector3.zero) return;
        Quaternion _rot = Quaternion.LookRotation(_lookDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _rot, Time.deltaTime * rotationSpeed);

    }
}
