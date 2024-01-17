using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AIP_IdleComponent : MonoBehaviour
{
    public event Action OnElapsed;
    [SerializeField] float timeMin = 0, timeMax = 3, waitingTime = 0,currentTime = 0;
    bool start = false;
    void Start()
    {
        OnElapsed += ResetTime;
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
    float UpdateTime(float _time, float _timeMax)
    {
        _time += Time.deltaTime;
        if (_time >= _timeMax)
        {
            OnElapsed?.Invoke();
            return 0;
        }
        return _time;
    }

    public void GetRandomWaitingTime()
    { 
        waitingTime = UnityEngine.Random.Range(timeMin, timeMax);
    }
    void Update()
    {
        if(start)
           currentTime = UpdateTime(currentTime,waitingTime);   
    }
}
