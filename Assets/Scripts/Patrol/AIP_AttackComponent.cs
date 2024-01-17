using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIP_AttackComponent : MonoBehaviour
{
    public event Action OnTargetDestroyed = null;
    public event Action OnAttackTimerElapsed = null;
    public event Action<bool> OnIsInRange = null;
    public event Action<bool> OnEnemyIsTooClose = null;
    [SerializeField] Transform enemyTransform = null;
    [SerializeField] Projectile projectile = null;
    [SerializeField] float currentTime = 0, maxTime = 1;
    [SerializeField] float damage = 1;
    [SerializeField] float range = 10;
    [SerializeField] float minRangeToEnemy = 7;
    [SerializeField] bool canAttack = true;
    private bool canStartTimer = true;

    public bool CanAttack
    { get
        {
            return canAttack;
        }
        set
        {
            canAttack = value;
        }
    }
    public bool IsInRange
    { get
        {
            if (!enemyTransform) return false;    // this our target
            float _dist = Vector3.Distance(enemyTransform.position, transform.position);

            if (_dist <= range)
            {
                OnIsInRange?.Invoke(true);
                Debug.Log("Enemy in range");
                return true;
            }
            OnIsInRange?.Invoke(false);
            Debug.Log("Enemy not in range");
            return false;
        }
    }
    public bool EnemyIsTooClose
    { get
        {
            if (!enemyTransform) return false;    // this our target
            float _dist = Vector3.Distance(enemyTransform.position, transform.position);
            Debug.Log($"Enemy distance = {_dist}");

            if (_dist <= minRangeToEnemy)
            {
                OnEnemyIsTooClose?.Invoke(true);
                return true;
            }
            OnEnemyIsTooClose?.Invoke(false);
            Debug.Log("Enemy is not to close, all good");
            return false;
        }
    }
    void Start()
    {
        OnAttackTimerElapsed += Attack;
    }


    void Update()
    {
        if (!canAttack) return;
        currentTime = UpdateTime(currentTime, maxTime);
       
    }

    public void SetTarget(Transform _target)
    {
        enemyTransform = _target;
    }

    void Attack()
    {
        if (!enemyTransform || !canAttack || !IsInRange) return;
        //Cast target
        // Deal damage
        Instantiate(projectile, transform.position,transform.rotation);
        //canAttack = false;
        //DestroyTarget();   // Testing without any damage just one shot kill;
    }

    void DestroyTarget()
    {
        Destroy(enemyTransform.gameObject);
        enemyTransform = null;
        OnTargetDestroyed?.Invoke();
    }

    float UpdateTime(float _currentTime, float _maxTime)
    {
        if (!canStartTimer) return 0;
        _currentTime += Time.deltaTime;
        if (_currentTime >= _maxTime)
        {
            canAttack = true;

            OnAttackTimerElapsed?.Invoke();
            return 0;
        }
        return _currentTime;

    }

    private void OnDrawGizmos()
    {
        AnmaGizmos.DrawSphere(transform.position, range, Color.cyan);
        AnmaGizmos.DrawSphere(transform.position, minRangeToEnemy, TVL_Colors.Colors.Chartreuse);
    }

}
