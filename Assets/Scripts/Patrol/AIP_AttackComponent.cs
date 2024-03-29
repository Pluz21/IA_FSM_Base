using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIP_AttackComponent : MonoBehaviour
{
    public event Action<GameObject> OnTargetDestroyed = null;
    public event Action<bool> OnIsInRange = null;
    [SerializeField] Transform enemyTransform = null;
    [SerializeField] float currentTime = 0, maxTime = 1;
    [SerializeField] float damage = 1;
    [SerializeField] float attackRange = 1;
    [SerializeField] bool canAttack = true;

    public bool IsInRange
    { get
        {
            if (!enemyTransform) return false;    // this our target
            if (Vector3.Distance(enemyTransform.position, transform.position) <= attackRange)
            {
                OnIsInRange?.Invoke(true);
                return true;
            }
            OnIsInRange?.Invoke(false);
            return false;
        }
    }
    void Start()
    {

    }


    void Update()
    {
        
        if (!canAttack) return;
        currentTime = UpdateTime(currentTime, maxTime);
    }


    public bool CheckIsInRange()
    {
        if (!enemyTransform) return false;    // this our target
        if (Vector3.Distance(enemyTransform.position, transform.position) <= attackRange)
        {
            OnIsInRange?.Invoke(true);
            return true;
        }
        OnIsInRange?.Invoke(false);
        return false;
    }
    public void SetTarget(Transform _target)
    {
        enemyTransform = _target;
    }

    public void Attack()
    {
        if (!enemyTransform || !canAttack) return;
        //Cast target
        // Deal damage
        canAttack = false;
        DestroyTarget();   // Testing without any damage just one shot kill;
        
    }

    void DestroyTarget()
    {
        OnTargetDestroyed?.Invoke(enemyTransform.gameObject);
        Destroy(enemyTransform.gameObject);
        enemyTransform = null;
    }

    float UpdateTime(float _currentTime, float _maxTime)
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= _maxTime)
        { 
            canAttack = true;
            return 0;
        }
        return _currentTime;

    }
}
