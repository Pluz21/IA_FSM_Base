using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIP_PatrolComponent : MonoBehaviour
{
    // this component will look for a point. 
    public event Action<Vector3> OnRandomLocationFound = null;   // MovementComponent will use the Vector3 from this event or the brain. 
    [SerializeField] Vector3 targetLocation = Vector3.zero;
    [SerializeField] float range = 20;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void FindRandomLocationInRange()
    {
        Vector2 _pos = UnityEngine.Random.insideUnitCircle * 2;
        targetLocation = transform.position + new Vector3(_pos.x, 0, _pos.y) * range;
        OnRandomLocationFound?.Invoke(targetLocation);
    }

    private void OnDrawGizmos()
    {
        AnmaGizmos.DrawSphere(transform.position, range, Color.blue);
        AnmaGizmos.DrawSphere(targetLocation, 0.5f, Color.red);
    }

}
