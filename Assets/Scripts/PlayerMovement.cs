using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _maxMovementSpeed = 10f;
    [SerializeField] private float _jumpForce = 500f;
    [SerializeField] private float _regularVelocity = 700f;
    [SerializeField] private Transform _followObject;
    [SerializeField] private Animator _animator;

    private float _inAirVelocity;
    private bool _inAir = false;
    public bool InAir => _inAir; 
    [SerializeField] private float _velocity;

    private Rigidbody _rigidbody;
    private PlayerRoll _playerRoll;
    
    private void Start() 
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerRoll = GetComponent<PlayerRoll>();

        _velocity = _regularVelocity;
        _inAirVelocity = _regularVelocity / 5;
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        RotateToCamera();
        HandleMovement();
        HandleMovementAnimation();
        HandleJumping();
        HandleAirMovementPenalty();
    }

    private void RotateToCamera()
    {
        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x,
            transform.localEulerAngles.y + _followObject.localEulerAngles.y,
            transform.localEulerAngles.z
        );
        _followObject.localEulerAngles = new Vector3(
            _followObject.localEulerAngles.x,
            0,
            _followObject.localEulerAngles.z
        );
    }

    private void HandleMovement()
    {
        if (_playerRoll.IsRolling) { return; }

        // ensures that the movement speed isn't larger than _maxMovementSpeed
        if (Vector3.Magnitude(_rigidbody.velocity) > _maxMovementSpeed) 
        {
            _rigidbody.velocity = Vector3.Normalize(_rigidbody.velocity) * _maxMovementSpeed;
            return;
        }

        Vector3 movementDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
        {
            movementDirection.x = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movementDirection.x = 1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            movementDirection.z = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movementDirection.z = -1;
        }
        movementDirection = Vector3.Normalize(movementDirection);
        
        _rigidbody.AddRelativeForce(movementDirection * _velocity * Time.deltaTime, ForceMode.VelocityChange);
    }

    private void HandleMovementAnimation()
    {
        _animator.SetBool("IsMoving", _rigidbody.velocity != Vector3.zero);
    }

    private void HandleJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_inAir && !_playerRoll.IsRolling)
        {
            _inAir = true;
            _rigidbody.AddRelativeForce(Vector3.up * _jumpForce);
        }
    }

    private void HandleAirMovementPenalty()
    {
        if (!_inAir)
        {
            _velocity = _regularVelocity;
        }
        else
        {
            _velocity = _inAirVelocity;
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (_inAir && other.gameObject.tag == "Platform")
        {
            _inAir = false;
        }
    }

    private void OnCollisionExit(Collision other) 
    {
        if (!_inAir && other.gameObject.tag == "Platform")
        {
            if (_rigidbody.velocity.y < 0)  // if gravity is moving the object down
            {
                _inAir = true;
            }
        }
    }
}
