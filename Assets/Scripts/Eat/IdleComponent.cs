using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleComponent : MonoBehaviour
{
    public event Action OnElapsed = null;
    [SerializeField] float minTime = 0, maxTime = 3, waitingTime = 0, currentTime = 0;
    bool start = false;
    void Start()
    {
        OnElapsed += ResetTime;
    }
    void Update()
    {
        if (start)
            currentTime = UpdateTime(currentTime, waitingTime);
    }
    public void GetTime()
    {
        waitingTime = UnityEngine.Random.Range(minTime, maxTime);
    }
    private void ResetTime()
    {
        start = false;
        currentTime = 0;
    }

    public void StartTime()
    {
        start = true;
    }

    float UpdateTime(float _time, float _maxTime)
    {
        _time += Time.deltaTime;
        if (_time >= _maxTime)
        {
            OnElapsed?.Invoke();
            return 0;
        }
        return _time;
        
    }
    // Update is called once per frame


}
