using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float moveSpeed = 20;
    [SerializeField] GameObject owner = null;
    [SerializeField] Enemy enemy =null;
    
    void Start()
    {
        Init();
    }

    void Init()
    {
        Destroy(transform.gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(transform.gameObject);
    }
}
