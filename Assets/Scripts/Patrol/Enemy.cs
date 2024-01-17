using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform playerTransform = null;
    [SerializeField] float moveSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveTo();
    }

    void MoveTo()
    { 
        Vector3 _direction = playerTransform.position - transform.position;
        transform.position += _direction * moveSpeed * Time.deltaTime;
    }
}
