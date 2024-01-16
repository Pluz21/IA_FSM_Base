using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LookForEnemyComponent : MonoBehaviour
{
    public event Action OnEnemyInRange;
    [SerializeField] bool inRange = false;
    [SerializeField] float detectionRange = 7;

    [SerializeField] EatComponent eatComponent = null;
    private bool canFindRandomPoint;

    public bool InRange => inRange;
    public float DetectionRange => detectionRange;

    
    // Start is called before the first frame update
    void Start()
    {
        eatComponent = GetComponent<EatComponent>();
        
        
    }

    void FindRandomPoint()
    {
        if (canFindRandomPoint == true)
        { 
        float _randX = UnityEngine.Random.Range(0.5f, 3);
        float _randY = UnityEngine.Random.Range(0.5f, 3);
        float _randZ = UnityEngine.Random.Range(0.5f, 3);
        Vector3 _point = new Vector3(_randX, _randY, _randZ);
        canFindRandomPoint = false;
        }
    }

    void Update()
    {
        if (!inRange)
        { 
            bool _inRange = CheckDistanceToDetect(GetClosest().position, transform.position);

            if (_inRange)
            {
                inRange = _inRange;
                OnEnemyInRange?.Invoke();
            }
        }

    }
    Transform GetClosest()
    {

        return eatComponent.AllFood.OrderBy(c => Vector3.Distance(c.position, transform.position)).First();   // Lambda to order list () equivalent
    }
    bool CheckDistanceToDetect(Vector3 _pos, Vector3 _targetPos)
    {
        float _distance = Vector3.Distance(_pos, _targetPos);
        Debug.Log($"{_distance}");
        if (_distance <= detectionRange)
            return true;
        return false;
    }
}
