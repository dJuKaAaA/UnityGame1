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
    
    private Rigidbody rb;
    private bool _inAir = false;

    private void Start() 
    {
        rb = GetComponent<Rigidbody>();
    } 

    private void Update()
    {
        GenerateMovement();
        GenerateJumping();
    }

    private void GenerateMovement()
    {
        // ensures that the movement speed isn't larger than _maxMovementSpeed
        if (Vector3.Magnitude(rb.GetRelativePointVelocity(Vector3.zero)) > _maxMovementSpeed) { return; }

        Vector3 movementVector = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
        {
            movementVector.x = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movementVector.x = 1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            movementVector.z = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movementVector.z = -1;
        }
        movementVector = Vector3.Normalize(movementVector) * _velocity * Time.deltaTime;
        
        rb.AddRelativeForce(movementVector);

        // Vector3 movementSpeedVector = rb.GetRelativePointVelocity(Vector3.zero);
        // Debug.Log(movementSpeedVector);
    }

    private void GenerateJumping()
    {
        if (!_inAir && Input.GetKeyDown(KeyCode.Space))
        {
            _velocity /= _inAirVelocityPenatly;
            _inAir = true;
            rb.AddRelativeForce(Vector3.up * _jumpForce);
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
