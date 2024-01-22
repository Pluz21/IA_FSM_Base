using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    MyInputs controls = null;

    InputAction move = null;
    InputAction rotate = null;

    [SerializeField] float moveSpeed = 5;
    [SerializeField] float rotateSpeed = 50;
    public InputAction Move => move;
    public InputAction Rotate => rotate;
    private void Awake()
    {
        controls = new MyInputs();
    }
    private void Start()
    {

    }

    private void Update()
    {
        MoveTo();
        RotateTo();

    }

    void Init()
    {
  
    }

    private void MoveTo()
    {
        Vector3 _moveDir = move.ReadValue<Vector3>();
        transform.position += transform.forward * moveSpeed * Time.deltaTime * _moveDir.z;
        transform.position += transform.right * moveSpeed * Time.deltaTime * _moveDir.x;

    }

    private void RotateTo()
    {
        float _rotateValue = rotate.ReadValue<float>();
        transform.eulerAngles += transform.up * _rotateValue * rotateSpeed * Time.deltaTime;

    }


    private void OnEnable()
    {
        move = controls.Player.Move;
        move.Enable();

        rotate = controls.Player.Rotate;
        rotate.Enable();

    }
}
