using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Security;

public class AIP_DetectionComponent : MonoBehaviour
{
    public event Action<GameObject> OnEntityDetected = null;
    
    [SerializeField] List<GameObject> allEntities = new List<GameObject>();
    [SerializeField] GameObject currentDetected = null;
    [SerializeField] float detectionRange = 10f;
    [SerializeField] bool debugCanDetect = true;  //only used in viewport


    public bool IsValid => allEntities.Count > 0;   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject GetClosest()
    {
        if (!IsValid) return null;
        Debug.Log("Closest grabbed");
        return allEntities.OrderBy(c => Vector3.Distance(c.transform.position, transform.position)).FirstOrDefault();   // Lambda to order list () equivalent
                                                                                                                                  // c is Transform type. Compare Distance from all 
    }

    public void Detect()
    {
        
        if (currentDetected) return;  // if we are already in range, no need.
        GameObject _target = GetClosest();
       // if (!_target) return;
        if (!_target && currentDetected || !debugCanDetect) 
        {
            Debug.Log("no target found");
           OnEntityDetected(null);
                return;
        }
        float _dist = Vector3.Distance(_target.transform.position, transform.position);
        currentDetected = _dist <= detectionRange ? _target: null;
        OnEntityDetected?.Invoke(currentDetected);

    }

    private void OnDrawGizmos()
    {
        AnmaGizmos.DrawSphere(transform.position, detectionRange, TVL_Colors.Colors.Chartreuse);
    }
}
