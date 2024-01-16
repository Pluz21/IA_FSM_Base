using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class PatrolComponent : MonoBehaviour

{
    public event Action OnPatrolReachedTarget;
    public event Action OnRandomPointSet;
    [SerializeField] Vector3 initialPos = Vector3.zero;
    [SerializeField] Vector3 target = Vector3.zero;
    [SerializeField] bool isAtLocation = false;
    [SerializeField] bool canMove= false;
    [SerializeField] float patrolSpeed = 10;
    [SerializeField] float rotationSpeed = 50;
    [SerializeField] float minDistance = 0.5f;
    private bool canFindRandomPoint;


    public bool CanFindRandomPoint
    {
        get { return CanFindRandomPoint;}
        set { canFindRandomPoint = value;}
    
    }


    public bool IsAtLocation
    {
        get
        {
            
            if (Vector3.Distance(transform.position, target) < minDistance)
                return true;
            return false;
        }
    }


    void Start()
    {
        OnRandomPointSet += SetCanMove;
        initialPos = transform.position;
        //FindRandomPoint();
    }
    void SetCanMove()
    { 
        canMove = true;
    }

  

    // Update is called once per frame
    void Update()
    {
        MoveTo();
        RotateTo();
    }
    public void FindRandomPoint()
    {
       
        float _randX = UnityEngine.Random.Range(0.6f, 3);
        float _randY = UnityEngine.Random.Range(0.6f, 3);
        float _randZ = UnityEngine.Random.Range(0.6f, 3);
        Vector3 _pos = new Vector3(transform.position.x + _randX, transform.position.y + _randY, transform.position.z + _randZ);
        target = _pos;
        OnRandomPointSet?.Invoke();


    }
    public void MoveTo()
    {
        if (!canMove && !IsAtLocation) return;
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * patrolSpeed);
        if (IsAtLocation)
        {
            OnPatrolReachedTarget?.Invoke();
            canMove = false;
        }


    }

    public void RotateTo()
    {
        if (IsAtLocation || !canMove) return;
        Vector3 _lookDirection = target - transform.position;
        if (_lookDirection == Vector3.zero) return;
        Quaternion _rot = Quaternion.LookRotation(_lookDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _rot, Time.deltaTime * rotationSpeed);

    }

    private void OnDrawGizmos()
    {
        
        AnmaGizmos.DrawSphere(target, 0.6f, Color.green);
    }
}
