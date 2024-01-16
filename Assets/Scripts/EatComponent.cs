using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EatComponent : MonoBehaviour
{
    public event Action<Transform> OnSelect = null;
    [SerializeField] List<Transform> allFood = new List<Transform>();
    [SerializeField] Transform target = null;
    [SerializeField] bool notCollected = false;


    public bool IsValid => allFood.Count > 0;
    public bool NotCollected => notCollected;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Transform GetClosest()
    { 
        if(!IsValid)return null;
        Debug.Log("Closest grabbed");
        return allFood.OrderBy(c => Vector3.Distance(c.position,transform.position)).First();   // Lambda to order list () equivalent
                                                                                                // c is Transform type. Compare Distance from all transforms 


    }
    public void Eat()
    {
        allFood.Remove(target);
        Destroy(target.gameObject);
        target = null;
    }

    public void Select()
    {
        if (target || !IsValid) return;
        target = GetClosest();
        Debug.Log("Selected");
        notCollected = true;
        OnSelect?.Invoke(target);
    }
}
