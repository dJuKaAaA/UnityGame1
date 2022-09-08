using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _maxMovementSpeed = 10f; 
    [SerializeField] private float _velocity = 500f;
    [SerializeField] private float _jumpForce = 500f;
    [Tooltip("When player is in the air, velocity is divided by this value and returned to normal when grounded")]
    [SerializeField] private float _inAirVelocityPenatly = 2.5f;  // decreases the velocity so the player can't move fast while in air
    

    private Rigidbody _rigidbody;
    private PlayerRoll _playerRoll;
    private bool _inAir = false;
    public bool InAir => _inAir;
    

    private void Start() 
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerRoll = GetComponent<PlayerRoll>();
    } 

    private void Update()
    {
        GenerateMovement();
        GenerateJumping();
    }

    private void GenerateMovement()
    {
        if (_playerRoll.IsRolling) { return; }

        // ensures that the movement speed isn't larger than _maxMovementSpeed
        if (Vector3.Magnitude(_rigidbody.velocity) > _maxMovementSpeed) { return; }

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
        
        _rigidbody.AddRelativeForce(movementDirection * _velocity * Time.deltaTime);
    }

    private void GenerateJumping()
    {
        if (!_inAir && Input.GetKeyDown(KeyCode.Space))
        {
            _velocity /= _inAirVelocityPenatly;
            _inAir = true;
            _rigidbody.AddRelativeForce(Vector3.up * _jumpForce);
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (_inAir && other.gameObject.tag == "Platform")
        {
            _velocity *= _inAirVelocityPenatly;
            _inAir = false;
        }
    }

    private void OnCollisionExit(Collision other) 
    {
        if (!_inAir && other.gameObject.tag == "Platform")
        {
            _inAir = true;
        }
    }
}
